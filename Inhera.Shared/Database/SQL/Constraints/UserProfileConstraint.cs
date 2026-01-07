using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Models.Database.SQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inhera.Shared.Database.SQL.Constraints
{
    public static class UserProfileConstraint
    {
        public static void Apply(ModelBuilder builder)
        {
            builder.Entity<UserProfileEntity>()
                .HasOne(e => e.Address)
                .WithOne()
                .HasForeignKey<UserProfileEntity>(e => e.AddressId);

            builder.Entity<UserProfileEntity>()
                .HasIndex(e => e.Email)
                .IsUnique();

            builder.Entity<UserProfileEntity>()
                .HasOne(e => e.Address)
                .WithOne(e => e.Profile)
                .HasForeignKey<UserProfileEntity>(e => e.AddressId);

            builder.Entity<AddressEntity>()
                .HasOne(e => e.Profile)
                .WithOne(e => e.Address)
                .HasForeignKey<AddressEntity>(e => e.ProfileId);
        }
    }
}
