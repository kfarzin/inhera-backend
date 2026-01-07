using Inhera.Authentication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Inhera.Authentication.Data
{
    public class AuthenticationContext(DbContextOptions<AuthenticationContext> options, IConfiguration configuration)
    : IdentityDbContext<AuthUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
        }
    }
}
