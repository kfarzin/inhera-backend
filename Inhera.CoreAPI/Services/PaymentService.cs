using Inhera.CoreAPI.Data;
using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Enums;
using Inhera.Shared.Models.Database.SQL.Entities;
using Inhera.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inhera.CoreAPI.Services
{
    public class PaymentService
    {
        private readonly SqlRepository<PaymentEntity, CoreContext> repository;
        private readonly StripePaymentService stripeService;
        private readonly SqlRepository<PaymentAttemptEntity, CoreContext> paymentAttemptRepository;
        private readonly ProfileService profileService;
        private readonly SubscriptionService subscriptionService;

        public PaymentService(
            SqlRepository<PaymentEntity, CoreContext> repository,
            SqlRepository<PaymentAttemptEntity, CoreContext> paymentAttemptRepository,
            ProfileService profileService,
            SubscriptionService subscriptionService,
            StripePaymentService stripeService)
        {
            this.repository = repository;
            this.paymentAttemptRepository = paymentAttemptRepository;
            this.profileService = profileService;
            this.subscriptionService = subscriptionService;
            this.stripeService = stripeService;
        }

        public async Task<PaymentAttemptEntity> CreatePaymentAttemptAsync(
            string stripePaymentIntentId,
            decimal amount,
            string currency,
            Guid userId,
            Guid? subscriptionId = null,
            string? description = null)
        {
            var payment = new PaymentAttemptEntity
            {
                StripePaymentIntentId = stripePaymentIntentId,
                Amount = amount,
                Currency = currency,
                Status = PaymentStatusTypes.Pending.ToString(),
                UserId = userId,
                SubscriptionId = subscriptionId,
                Description = description
            };

            await paymentAttemptRepository.Add(payment, saveChanges: true);

            return payment;
        }

        public async Task<PaymentEntity?> GetPaymentByStripeIdAsync(string stripePaymentIntentId)
        {
            return await repository.GetRawRepository()
                .FirstOrDefaultAsync(p => p.StripePaymentIntentId == stripePaymentIntentId);
        }

        public async Task<PaymentEntity?> GetPaymentByIdAsync(Guid id)
        {
            return await repository.GetById(id.ToString());
        }

        public async Task<List<PaymentEntity>> GetUserPaymentsAsync(Guid userId)
        {
            return await repository.GetRawRepository()
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task UpdatePaymentStatusAsync(
            string stripePaymentIntentId,
            PaymentStatusTypes status,
            string? failureReason = null,
            DateTimeOffset? paidAt = null)
        {
            var payment = await GetPaymentByStripeIdAsync(stripePaymentIntentId);
            if (payment == null)
            {
                return;
            }

            payment.Status = status.ToString();
            payment.FailureReason = failureReason;
            payment.PaidAt = paidAt ?? (status == PaymentStatusTypes.Succeeded ? DateTimeOffset.UtcNow : null);

            await repository.Update(payment, saveChanges: true);
        }

        public async Task<PaymentAttemptEntity> ProcessPaymentAsync(
            decimal amount,
            string currency,
            Guid userId,
            Guid? subscriptionId = null,
            string? description = null)
        {
            // Create Stripe PaymentIntent
            var paymentIntent = await stripeService.CreatePaymentIntentAsync(
                amount, currency, userId, subscriptionId, description);

            // Store payment record in database
            var payment = await CreatePaymentAttemptAsync(
                paymentIntent.Id,
                amount,
                currency,
                userId,
                subscriptionId,
                description);

            return payment;
        }

        public async Task<bool?> ProcessPaymentByPaymentAttempt(string paymentIntentId)
        {
            // Validate payment with Stripe first
            var paymentStatus = await stripeService.GetPaymentIntentStatusAsync(paymentIntentId);
            
            if (paymentStatus != "succeeded")
            {
                throw new Exception($"Payment is not completed. Current status: {paymentStatus}");
            }

            // Find the PaymentAttempt by the payment intent
            var paymentAttempt = await paymentAttemptRepository.GetRawRepository()
                .FirstOrDefaultAsync(pa => pa.StripePaymentIntentId == paymentIntentId);

            if (paymentAttempt == null)
            {
                return false;
            }

            var existingPayment = await repository.GetRawRepository()
                .FirstOrDefaultAsync(e => e.SubscriptionId == paymentAttempt.SubscriptionId);

            if (existingPayment != null)
            {
                return true;
            }
            // Create a Payment entity with the same content as the PaymentAttempt
            var payment = new PaymentEntity
            {
                StripePaymentIntentId = paymentAttempt.StripePaymentIntentId,
                Amount = paymentAttempt.Amount,
                Currency = paymentAttempt.Currency,
                Status = PaymentStatusTypes.Succeeded.ToString(),
                SubscriptionId = paymentAttempt.SubscriptionId,
                UserId = paymentAttempt.UserId,
                StripeCustomerId = paymentAttempt.StripeCustomerId,
                PaymentMethodId = paymentAttempt.PaymentMethodId,
                FailureReason = paymentAttempt.FailureReason,
                PaidAt = DateTimeOffset.UtcNow,
                Description = paymentAttempt.Description,
                ReceiptUrl = paymentAttempt.ReceiptUrl
            };

            var profile = await profileService.GetProfileByUserId(payment.UserId);
            if(profile == null)
            {
                //TODO: Log this
                return false;
            }
            profile.OnboardingStep = GetNextStep(profile);

            var subscription = await subscriptionService.GetCurrentPendingSubscription(profile);
            if (subscription == null)
            {
                //TODO: Log this
                return false;
            }
            subscription.SubscriptionBookingStatus = SubscriptionBookingStatusTypes.Booked.ToString();

            await repository.Add(payment);
            await repository.SaveChangesAsync();

            return true;
        }

        private string GetNextStep(UserProfileEntity profile)
        {
            var anyCollectionalService = profile.Subscriptions.FirstOrDefault()
                ?.Services.Any(e => e?.AdditionalService?.Type == AdditionalServiceTypes.Collection.ToString());
            if (anyCollectionalService == true)
            {
                return OnboardingStepTypes.CollectionAppointment.ToString();
            }                
            return OnboardingStepTypes.LabAppointment.ToString();
        }
    }
}
