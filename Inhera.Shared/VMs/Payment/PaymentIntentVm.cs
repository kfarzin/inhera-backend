namespace Inhera.Shared.VMs.Payment
{
    public class PaymentIntentVm
    {
        public required string ClientSecret { get; set; }
        public required string PaymentIntentId { get; set; }
        public decimal Amount { get; set; }
        public required string Currency { get; set; }
        //public required string Status { get; set; }
    }
}
