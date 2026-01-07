using Inhera.NotificationService.Models.Enums;
using Inhera.Shared.Database.SQL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;


namespace Inhera.NotificationService.Models.Entities.SQL
{
    public class GenericDeliverableMessage: SqlEntity
    {
        public static readonly int MaximumNumberOfDeliveryTries = 5;
        public string? ModelType { get; set; }

        [Column(TypeName = "jsonb")]
        public string? Data { get; set; }
        public string? Template {  get; set; }
        public string? Subject { get; set; }
        public int SecondsBeforeMessageIsSent { get; set; }
        public GenericDeliverableMessagePriorityTypes Priority { get; set; } = GenericDeliverableMessagePriorityTypes.Low;       
        public string? RequestedBy {  get; set; }
        public MessageDeliveryTypes MessageDeliveryType { get; set; }
        public int MaxNumberOfDeliveryTries { get; set; } = MaximumNumberOfDeliveryTries;
        public int TriedDeliveryFor { get; set; }
        public bool IsDelivered { get; set; } = false;
        public DateTimeOffset DeliveredAt { get; set; }
        public DateTimeOffset LastTryAt { get; set; }

        [Column(TypeName = "jsonb")]
        public string? Audiences { get; set; } //to

        [Column(TypeName = "jsonb")]
        public string? Ccs { get; set; }

        [Column(TypeName = "jsonb")]
        public string? Bccs { get; set; }

        public static string ToJsonbString(object model)
        {
            return JsonSerializer.Serialize(model);
        }

        public static string ToJsonbString(params List<string> items)
        {
            return JsonSerializer.Serialize(items);
        }
    }

    public class GenericDeliverableMessageModelTypes
    {
        public static readonly string TestType = "TestType";
    }
}
