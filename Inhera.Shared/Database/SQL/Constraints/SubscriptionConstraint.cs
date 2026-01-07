using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Models.Database.SQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inhera.Shared.Database.SQL.Constraints
{
    public static class SubscriptionConstraint
    {
        public static void Apply(ModelBuilder builder)
        {
            builder.Entity<SubscriptionEntity>()
                .HasOne(e => e.Plan)
                .WithMany(e => e.Subscriptions);

            builder.Entity<SubscriptionEntity>()
                .HasOne(e => e.UserProfile)
                .WithMany(e => e.Subscriptions)
                .HasForeignKey(e => e.UserProfileId);

            builder.Entity<SubscriptionEntity>()
                .HasOne(e => e.Plan)
                .WithMany(e => e.Subscriptions)
                .HasForeignKey(e => e.PlanId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SubscriptionAdditionalServiceEntity>()
                .HasOne(e => e.Subscription)
                .WithMany(e => e.Services)
                .HasForeignKey(e => e.SubscriptionId);

            builder.Entity<SubscriptionAdditionalServiceEntity>()
                .HasOne(e => e.AdditionalService)
                .WithMany(e => e.SubscriptionServices)
                .HasForeignKey(e => e.AdditionalServiceId);

            builder.Entity<SubscriptionEntity>()
                .HasOne(e => e.Payment)
                .WithOne(e => e.Subscription)
                .HasForeignKey<SubscriptionEntity>(e => e.PaymentId);
        }
    }
}
