using Inhera.Shared.Database.SQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inhera.Shared.Database.SQL.Constraints
{
    public static class LabCenterConstraint
    {
        public static void Apply(ModelBuilder builder)
        {
            builder.Entity<LabCenterEntity>()
                .HasIndex(e => new { e.Latitude, e.Longitude });

            builder.Entity<LabCenterCalendarEntity>()
                .Property(e => e.TimeSlots)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<LabCenterTimeSlot>>(v, (System.Text.Json.JsonSerializerOptions?)null)!
                );

            builder.Entity<LabCenterCalendarEntity>()
                .HasIndex(e => new { e.LabCenterId, e.DateIdentifier })
                .IsUnique();
        }
    }
}