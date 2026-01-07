using Inhera.CoreAPI.Services;
using Inhera.Shared.ModelMappers;
using Inhera.Shared.Services;
using Inhera.Shared.VMs.Payment;

namespace Inhera.CoreAPI.ResponderServices
{
    public class PaymentResponderService : BaseResponderService
    {
        private readonly PaymentService paymentService;
        private readonly StripePaymentService stripePaymentService;
        private readonly ProfileService profileService;
        private readonly SubscriptionService subscriptionService;

        public PaymentResponderService(
            PaymentService paymentService,
            StripePaymentService stripePaymentService,
            ProfileService profileService,
            SubscriptionService subscriptionService)
        {
            this.paymentService = paymentService;
            this.stripePaymentService = stripePaymentService;
            this.profileService = profileService;
            this.subscriptionService = subscriptionService;
        }

        public async Task<ServiceContainer<PaymentStatusVm>> ValidatePayment(Guid userId, PaymentValidationVvm model)
        {
            try
            {
                var payment = await paymentService.ProcessPaymentByPaymentAttempt(model.PaymentIntentId);

                if (payment == false)
                {
                    return BadContainer<PaymentStatusVm>(null, "Payment attempt not found with the provided intent ID");
                }

               var result = new PaymentStatusVm { Status = true };
                return OkContainer(result);
            }
            catch (Exception ex)
            {
                return BadContainer<PaymentStatusVm>(null, LogException(ex));
            }
        }

        public async Task<ServiceContainer<PaymentIntentVm>> CreatePaymentIntentForActivePlan(
            Guid userId)
        {
            try
            {
                var profile = await profileService.GetProfileByUserId(userId) ?? throw new Exception("wrong profile");
                var currentPendingSubscription = await subscriptionService.GetCurrentPendingSubscription(profile);

                var plan = currentPendingSubscription?.Plan ?? throw new Exception("no pending plan");

                var payment = await paymentService.ProcessPaymentAsync(
                    plan.PriceInCents,
                    plan.Currency,
                    userId,
                    currentPendingSubscription.Id,
                    plan.Description);

                // Get the Stripe PaymentIntent to return client secret
                var paymentIntent = await stripePaymentService.GetPaymentIntentAsync(payment.StripePaymentIntentId);

                var result = new PaymentIntentVm
                {
                    ClientSecret = paymentIntent.ClientSecret,
                    PaymentIntentId = paymentIntent.Id,
                    Amount = plan.PriceInCents,
                    Currency = plan.Currency,
                    //Status = paymentIntent.Status
                };

                return OkContainer(result);
            }
            catch (Exception ex)
            {
                return BadContainer<PaymentIntentVm>(null, LogException(ex));
            }
        }

        public async Task<ServiceContainer<PaymentIntentVm>> CreatePaymentIntent(
            Guid userId,
            CreatePaymentIntentVvm model)
        {
            try
            {
                var payment = await paymentService.ProcessPaymentAsync(
                    model.Amount,
                    model.Currency,
                    userId,
                    model.SubscriptionId,
                    model.Description);

                // Get the Stripe PaymentIntent to return client secret
                var paymentIntent = await stripePaymentService.GetPaymentIntentAsync(payment.StripePaymentIntentId);

                var result = new PaymentIntentVm
                {
                    ClientSecret = paymentIntent.ClientSecret,
                    PaymentIntentId = paymentIntent.Id,
                    Amount = model.Amount,
                    Currency = model.Currency,
                    //Status = paymentIntent.Status
                };

                return OkContainer(result);
            }
            catch (Exception ex)
            {
                return BadContainer<PaymentIntentVm>(null, LogException(ex));
            }
        }

        public async Task<ServiceContainer<PaymentVm>> GetPaymentById(Guid paymentId)
        {
            try
            {
                var payment = await paymentService.GetPaymentByIdAsync(paymentId);
                if (payment == null)
                {
                    return BadContainer<PaymentVm>(null, "Payment not found");
                }

                var result = payment.ToPaymentVm();
                return OkContainer(result);
            }
            catch (Exception ex)
            {
                return BadContainer<PaymentVm>(null, LogException(ex));
            }
        }

        public async Task<ServiceContainer<List<PaymentVm>>> GetUserPayments(Guid userId)
        {
            try
            {
                var payments = await paymentService.GetUserPaymentsAsync(userId);
                var result = payments.Select(p => p.ToPaymentVm()).ToList();
                return OkContainer(result);
            }
            catch (Exception ex)
            {
                return BadContainer<List<PaymentVm>>(null, LogException(ex));
            }
        }

        public Task<ServiceContainer<SubscriptionReviewVm>> GetSubscriptionReview(Guid userId)
        {
            try
            {
                var mockData = new SubscriptionReviewVm
                {
                    Profile = new PaymentProfileVm
                    {
                        Title = "Mr",
                        FirstName = "John",
                        LastName = "Doe",
                        CustomerNumber = "CUST-12345",
                        PhoneNumber = "+49 123 456789",
                        MobileNumber = "+49 987 654321",
                        Gender = "male",
                        DateOfBirth = "1990-05-15",
                        Email = "john.doe@example.com",
                        IsBoardingCompleted = false,
                        OnboardingStep = "review",
                        Country = "Germany",
                        Address = new PaymentAddressVm
                        {
                            Title = "Mr",
                            FirstName = "John",
                            LastName = "Doe",
                            PhoneNumber = "+49 123 456789",
                            MobileNumber = "+49 987 654321",
                            Email = "john.doe@example.com",
                            IsDefault = true,
                            Street = "Hauptstrasse",
                            HouseNo = "123",
                            Additional1 = "Apt 4B",
                            Additional2 = "",
                            ZipCode = "10115",
                            City = "Berlin",
                            Country = "Germany"
                        }
                    },
                    SelectedPlan = new PaymentPlanVm
                    {
                        Id = "plan-premium-001",
                        Code = "PREMIUM_MONTHLY",
                        Name = "Premium",
                        Description = "Full access to all features",
                        FeaturesSummary = "Unlimited consultations, Priority support, Advanced analytics, Custom reports",
                        Currency = "EUR",
                        PriceInCents = 4900,
                        BillingCycle = "Monthly",
                        ApplicableCountry = "Germany",
                        CreatedAt = "2025-01-01T00:00:00Z",
                        UpdatedAt = "2025-01-01T00:00:00Z",
                        IsActive = true
                    },
                    SelectedAdditionalServices = new List<PaymentAdditionalServiceVm>
                    {
                        new PaymentAdditionalServiceVm
                        {
                            Id = "service-doctor-001",
                            Code = "DOCTOR_APPOINTMENT_60",
                            Name = "Doctor Consultation Package",
                            Description = "60-minute video consultation with a specialist",
                            FeaturesSummary = "Video consultation, Follow-up support, Medical records",
                            Currency = "EUR",
                            PriceInCents = 8900,
                            ApplicableCountry = "Germany",
                            CreatedAt = "2025-01-01T00:00:00Z",
                            UpdatedAt = "2025-01-01T00:00:00Z",
                            IsActive = true
                        },
                        new PaymentAdditionalServiceVm
                        {
                            Id = "service-collection-001",
                            Code = "BLOOD_COLLECTION_HOME",
                            Name = "At-Home Blood Collection",
                            Description = "Professional blood collection service at your home",
                            FeaturesSummary = "Home visit, Lab processing, Digital results",
                            Currency = "EUR",
                            PriceInCents = 4500,
                            ApplicableCountry = "Germany",
                            CreatedAt = "2025-01-01T00:00:00Z",
                            UpdatedAt = "2025-01-01T00:00:00Z",
                            IsActive = true
                        }
                    },
                    Pricing = new PaymentPricingVm
                    {
                        PlanPrice = 4900,
                        AdditionalServicesPrice = 13400,
                        Subtotal = 18300,
                        Tax = 3477,
                        TaxRate = 19,
                        Total = 21777,
                        Currency = "EUR"
                    }
                };

                return Task.FromResult(OkContainer(mockData));
            }
            catch (Exception ex)
            {
                return Task.FromResult(BadContainer<SubscriptionReviewVm>(null, LogException(ex)));
            }
        }
    }
}
