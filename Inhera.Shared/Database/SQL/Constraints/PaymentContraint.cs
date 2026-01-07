using Inhera.Shared.Database.SQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inhera.Shared.Database.SQL.Constraints
{
    public static class PaymentContraint
    {
        public static void Apply(ModelBuilder builder)
        {
            builder.Entity<PaymentEntity>()
                .HasOne(e => e.Subscription)
                .WithOne(e => e.Payment)
                .HasForeignKey<PaymentEntity>(e => e.SubscriptionId);

            builder.Entity<PaymentAttemptEntity>()
                .HasOne(e => e.Subscription)
                .WithMany(e => e.PaymentAttempts)
                .HasForeignKey(e => e.SubscriptionId);            
        }
    }
}
