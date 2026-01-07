using Inhera.Shared.Database.SQL.Constraints;
using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Models.Database.SQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inhera.CoreAPI.Data
{

    public class CoreContext : DbContext
    {
        private readonly IConfiguration configuration;
        public required DbSet<UserProfileEntity> UserProfiles { get; init; }

        public required DbSet<PlanEntity> Plans { get; init; }
        public required DbSet<SubscriptionEntity> Subscriptions { get; init; }
        public required DbSet<AdditionalServiceEntity> AdditionalServices { get; init; }
        public required DbSet<SubscriptionAdditionalServiceEntity> SubscriptionServices { get; init; }
        public required DbSet<PaymentEntity> Payments { get; init; }
        public required DbSet<PaymentAttemptEntity> PaymentAttempts { get; init; }
        public required DbSet<LabCenterCalendarEntity> LabCenterCalendars { get; init; }


        public CoreContext(DbContextOptions<CoreContext> options, IConfiguration configuration)
        : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            UserProfileConstraint.Apply(modelBuilder);
            LabCenterConstraint.Apply(modelBuilder);
            PlanConstraint.Apply(modelBuilder);
            SubscriptionConstraint.Apply(modelBuilder);
            PaymentContraint.Apply(modelBuilder);
        }

        public override int SaveChanges()
        {
            ApplyTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }


        private void ApplyTimestamps()
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<SqlEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.IsActive = true;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.CreatedAt).IsModified = false;
                    entry.Entity.UpdatedAt = now;
                }
            }
        }
    }
}
