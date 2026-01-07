using System.ComponentModel.DataAnnotations.Schema;

namespace Inhera.Shared.Database.SQL.Entities
{
    [Table("LabCenterCalendars")]
    public class LabCenterCalendarEntity : SqlEntity
    {
        public Guid LabCenterId { get; set; }
        public LabCenterEntity? LabCenter { get; set; }
        public DateOnly DateIdentifier { get; set; }

        [Column(TypeName = "jsonb")]
        public List<LabCenterTimeSlot> TimeSlots { get; set; } = [];
    }

    public class LabCenterTimeSlot
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsReserved { get; set; }        
    }
}
