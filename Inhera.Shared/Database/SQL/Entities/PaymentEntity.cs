using Inhera.Shared.Enums;
using Inhera.Shared.Models.Database.SQL.Entities;
using Inhera.Shared.Util.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inhera.Shared.Database.SQL.Entities
{
    [Table("Payments")]
    public class PaymentEntity : SqlEntity
    {
        public required string StripePaymentIntentId { get; set; }
        public required decimal Amount { get; set; }
        public required string Currency { get; set; }
        
        [EnumStringValue(typeof(PaymentStatusTypes))]
        public required string Status { get; set; }
        
        public Guid? SubscriptionId { get; set; }
        public SubscriptionEntity? Subscription { get; set; }
        
        public Guid UserId { get; set; }
        public UserProfileEntity User { get; set; } = null!;
        
        public string? StripeCustomerId { get; set; }
        public string? PaymentMethodId { get; set; }
        public string? FailureReason { get; set; }
        public DateTimeOffset? PaidAt { get; set; }
        
        public string? Description { get; set; }
        public string? ReceiptUrl { get; set; }
    }
}
