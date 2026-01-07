using System.ComponentModel.DataAnnotations.Schema;

namespace Inhera.Shared.Database.SQL.Entities
{
    public class LabCenterEntity : SqlEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

        public AddressEntity? Address { set; get; }
        public Guid? AddressId { set; get; }

        
        [Column(TypeName = "decimal(9,6)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal Longitude { get; set; }

        //TODO: needs to be refactored later on according to the requirements
        public string? OperationHours { get; set; }
        public string? OperationDays { get; set; }

        public string? ContactNumber { get; set; }
        public string? ContactName { get; set; }

        public List<LabCenterCalendarEntity> Calendars = [];
        public string? AccessKey { get; set; }
        public bool HasAccess { get; set; } = true;
    }
}
