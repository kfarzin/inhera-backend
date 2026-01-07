using Inhera.Shared.Enums;
using Inhera.Shared.Models.Database.SQL.Entities;
using Inhera.Shared.Util.Common;
using System.Collections.ObjectModel;

namespace Inhera.Shared.Database.SQL.Entities
{
    public class SubscriptionEntity : SqlEntity
    {
        public Guid PlanId { get; set; }
        public PlanEntity? Plan { get; set; }

        public Guid UserProfileId { get; set; }
        public UserProfileEntity? UserProfile { get; set; }

        public Guid? PaymentId { get; set; }
        public PaymentEntity? Payment { get; set; }

        public bool AutoRenew { get; set; } = false;

        // Periods (UTC)
        public required DateTimeOffset SelectedAtUtc { get; set; }
        public DateTimeOffset StartAtUtc { get; set; }
        public DateTimeOffset EndAtUtc { get; set; }

        // Cancellation        
        public DateTimeOffset? CanceledAtUtc { get; set; }

        [EnumStringValue(typeof(SubscriptionBookingStatusTypes))]
        public required string SubscriptionBookingStatus { get; set; } = SubscriptionBookingStatusTypes.Pending.ToString();

        public Collection<SubscriptionAdditionalServiceEntity> Services { get; set; } = [];
        public Collection<PaymentAttemptEntity> PaymentAttempts { get; set; } = [];

    }
}
