using Inhera.Shared.Database.SQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inhera.Shared.Database.SQL.Constraints
{
    public static class PlanConstraint
    {
        public static void Apply(ModelBuilder builder)
        {
            builder.Entity<PlanEntity>()
                .HasMany(e => e.Subscriptions)
                .WithOne(e => e.Plan)
                .HasForeignKey(e => e.PlanId);
        }
    }
}
