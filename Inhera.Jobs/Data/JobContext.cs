using Microsoft.EntityFrameworkCore;

namespace Inhera.Jobs.Data
{
    public class JobContext : DbContext
    {
        private readonly IConfiguration configuration;        

        public JobContext(DbContextOptions<JobContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
