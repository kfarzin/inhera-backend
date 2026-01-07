using Inhera.Jobs.Data;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using Microsoft.Extensions.Logging;

namespace Inhera.Jobs.Config
{
    public static class AppConfig
    {
        public static void Init(WebApplicationBuilder builder)
        {
            RegisterServices(builder);
            AddLogSupport(builder);
        }

        public static void RegisterServices(WebApplicationBuilder builder)
        {
            var PostgresConnection = builder.Configuration.GetConnectionString("PostgresConnection");
            builder.Services.AddDbContext<JobContext>(options =>
            options.UseNpgsql(PostgresConnection));

            builder.Services.AddScoped<JobContext>();
        }

        public static void AddQuartzSupport(WebApplicationBuilder builder)
        {
            builder.Services.AddQuartz();
            builder.Services.AddQuartzHostedService(opt =>
            {
                opt.WaitForJobsToComplete = false;
            });
        }

        private static void AddLogSupport(WebApplicationBuilder builder)
        {
            //Serilog.ILogger logger = new LoggerConfiguration()
            //        .WriteTo.GrafanaLoki(
            //        "http://localhost:3100")
            //        .CreateLogger();
            //builder.Services.AddScoped<Serilog.ILogger>(e => logger);

            // Configure Serilog as the global logger
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.GrafanaLoki(
                    "http://localhost:3100",
                    new List<LokiLabel> { new() { Key = "app", Value = "jobs service" } })
                .CreateLogger();

            // Use Serilog as the logging provider for Microsoft.Extensions.Logging
            builder.Host.UseSerilog();

        }
    }
}
