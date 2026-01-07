namespace Inhera.Shared.Database.SQL.Entities
{
    public class SubscriptionAdditionalServiceEntity : SqlEntity
    {
        public required Guid SubscriptionId { set; get; }
        public SubscriptionEntity? Subscription { set; get; }
        public required Guid AdditionalServiceId { set; get; }
        public AdditionalServiceEntity? AdditionalService { set; get; }
    }
}
