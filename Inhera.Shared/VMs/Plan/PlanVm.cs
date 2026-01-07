namespace Inhera.Shared.VMs.Plan
{
    public class PlanVm
    {
        public Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? FeaturesSummary { get; set; }
        public required string Currency { get; set; }
        public int PriceInCents { get; set; }
        public string BillingCycle { get; set; } = string.Empty;
        public required string ApplicableCountry { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
