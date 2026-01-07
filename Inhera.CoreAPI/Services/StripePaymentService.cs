using Stripe;

namespace Inhera.CoreAPI.Services
{
    public class StripePaymentService
    {
        private readonly string secretKey;

        public StripePaymentService(IConfiguration config)
        {
            secretKey = config["Stripe:SecretKey"] ?? throw new ArgumentNullException("Stripe:SecretKey");
            StripeConfiguration.ApiKey = secretKey;
        }

        public async Task<PaymentIntent> CreatePaymentIntentAsync(
            decimal amount,
            string currency,
            Guid userId,
            Guid? subscriptionId = null,
            string? description = null)
        {
            var metadata = new Dictionary<string, string>
            {
                { "user_id", userId.ToString() }
            };

            if (subscriptionId.HasValue)
            {
                metadata.Add("subscription_id", subscriptionId.Value.ToString());
            }

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Convert to cents
                Currency = currency.ToLower(),
                Description = description,
                Metadata = metadata,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return paymentIntent;
        }

        public async Task<PaymentIntent> GetPaymentIntentAsync(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            return await service.GetAsync(paymentIntentId);
        }

        public async Task<string> GetPaymentIntentStatusAsync(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            var paymentIntent = await service.GetAsync(paymentIntentId);
            return paymentIntent.Status;
        }

        public async Task<PaymentIntent> CancelPaymentIntentAsync(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            var paymentIntent = await service.CancelAsync(paymentIntentId);

            return paymentIntent;
        }

        public async Task<Subscription> CreateSubscriptionAsync(
            string customerId,
            string priceId,
            Guid userId)
        {
            var options = new SubscriptionCreateOptions
            {
                Customer = customerId,
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions { Price = priceId }
                },
                PaymentBehavior = "default_incomplete",
                PaymentSettings = new SubscriptionPaymentSettingsOptions
                {
                    SaveDefaultPaymentMethod = "on_subscription"
                },
                Metadata = new Dictionary<string, string>
                {
                    { "user_id", userId.ToString() }
                }
            };

            var service = new Stripe.SubscriptionService();
            var subscription = await service.CreateAsync(options);

            return subscription;
        }

        public async Task<Customer> CreateCustomerAsync(string email, Guid userId, string? name = null)
        {
            var options = new CustomerCreateOptions
            {
                Email = email,
                Name = name,
                Metadata = new Dictionary<string, string>
                {
                    { "user_id", userId.ToString() }
                }
            };

            var service = new CustomerService();
            var customer = await service.CreateAsync(options);

            return customer;
        }

        public async Task<Refund> CreateRefundAsync(string paymentIntentId, long? amount = null)
        {
            var options = new RefundCreateOptions
            {
                PaymentIntent = paymentIntentId,
                Amount = amount // If null, refunds full amount
            };

            var service = new RefundService();
            var refund = await service.CreateAsync(options);

            return refund;
        }
    }
}
