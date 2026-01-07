namespace Inhera.Shared.VMs.AdditionalService
{
    public class AdditionalServiceVm
    {
        public Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? FeaturesSummary { get; set; }
        public required string Currency { get; set; }
        public int PriceInCents { get; set; }
        public required string ApplicableCountry { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
