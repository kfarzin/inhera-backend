using Inhera.NotificationService.Models.Entities.SQL;
using Microsoft.EntityFrameworkCore;

namespace Inhera.NotificationService.Data
{
    public class MainContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DbSet<CounterEntity> Counters { get; set; }
        public DbSet<GenericDeliverableMessage> genericDeliverableMessages { get; set; }

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
        }
    }
}
