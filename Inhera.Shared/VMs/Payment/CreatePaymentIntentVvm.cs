namespace Inhera.Shared.VMs.Payment
{
    public class CreatePaymentIntentVvm
    {
        public Guid SubscriptionId { get; set; }
        public decimal Amount { get; set; }
        public required string Currency { get; set; }
        public string? Description { get; set; }
    }
}
