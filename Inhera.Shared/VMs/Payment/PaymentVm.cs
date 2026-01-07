namespace Inhera.Shared.VMs.Payment
{
    public class PaymentVm
    {
        public Guid Id { get; set; }
        public required string StripePaymentIntentId { get; set; }
        public decimal Amount { get; set; }
        public required string Currency { get; set; }
        public required string Status { get; set; }
        public Guid? SubscriptionId { get; set; }
        public Guid UserId { get; set; }
        public string? FailureReason { get; set; }
        public DateTimeOffset? PaidAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
