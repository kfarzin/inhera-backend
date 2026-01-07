using Inhera.Shared.Database.SQL.Constraints;
using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Models.Database.SQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inhera.Infrastructure.Data
{
    public class MainContext : DbContext
    {
        private readonly IConfiguration configuration;
        public required DbSet<UserProfileEntity> UserProfiles { get; init; }
        public required DbSet<AddressEntity> Addresses { get; init; }
        public required DbSet<LabCenterEntity> LabCenters { get; init; }

        public required DbSet<PlanEntity> Plans { get; init; }
        public required DbSet<SubscriptionEntity> Subscriptions { get; init; }
        public required DbSet<AdditionalServiceEntity> AdditionalServices { get; init; }
        public required DbSet<SubscriptionAdditionalServiceEntity> SubscriptionServices { get; init; }
        public required DbSet<LabCenterCalendarEntity> LabCenterCalendars { get; init; }
        public required DbSet<PaymentEntity> Payments { get; init; }
        public required DbSet<PaymentAttemptEntity> PaymentAttempts { get; init; }



        public MainContext(DbContextOptions<MainContext> options, IConfiguration configuration)
        : base(options)
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            UserProfileConstraint.Apply(builder);
            LabCenterConstraint.Apply(builder);
            PlanConstraint.Apply(builder);
            SubscriptionConstraint.Apply(builder);
            PaymentContraint.Apply(builder);
        }
    }
}
