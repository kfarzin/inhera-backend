using Inhera.Shared.Enums;
using Inhera.Shared.Util.Common;

namespace Inhera.Shared.Database.SQL.Entities
{
    public class PlanEntity : SqlEntity
    {
        public required string Code { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? FeaturesSummary { get; set; }

        public required string Currency { get; set; }
        public int PriceInCents { get; set; }    // e.g., 999 for $9.99`

        [EnumStringValue(typeof(BillingCycleTypes))]
        public string BillingCycle { get; set; } = BillingCycleTypes.Monthly.ToString();
        
        
        [EnumStringValue(typeof(PlanCountryTypes))]
        public required string ApplicableCountry { get; set; }

        //if needed
        //public ICollection<PlanFeatureEntity> Features { get; set; } = new List<PlanFeature>();

        public ICollection<SubscriptionEntity> Subscriptions { get; set; } = [];
    }
}
