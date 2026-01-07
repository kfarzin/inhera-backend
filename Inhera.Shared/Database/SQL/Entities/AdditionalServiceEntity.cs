using Inhera.Shared.Enums;
using Inhera.Shared.Util.Common;
using System.Collections.ObjectModel;

namespace Inhera.Shared.Database.SQL.Entities
{
    public class AdditionalServiceEntity : SqlEntity
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? FeaturesSummary { get; set; }
        public required string Currency { get; set; }
        public int PriceInCents { get; set; }    // e.g., 999 for $9.99`

        [EnumStringValue(typeof(AdditionalServiceTypes))]
        public required string Type { set; get; } = AdditionalServiceTypes.InPerson.ToString();

        public required string ApplicableCountry { get; set; }

        public Collection<SubscriptionAdditionalServiceEntity> SubscriptionServices { get; set; } = [];
    }
}