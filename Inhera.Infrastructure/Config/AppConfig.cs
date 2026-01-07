using Inhera.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Inhera.Infrastructure.Config
{
    public static class AppConfig
    {
        //private const string OutputTemplate = "{Timestamp:dd-MM-yyyy HH:mm:ss} [{Level:u3}] [{ThreadId}] {Message}{NewLine}{Exception}";
        public static void Init(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<AppEnvironmentSetting>();
            var configuration = new AppEnvironmentSetting(builder.Environment);
            RegisterServices(builder);

            var PostgresConnection = builder.Configuration.GetConnectionString("PostgresConnection");
            builder.Services.AddDbContext<MainContext>(options =>
            options.UseNpgsql(PostgresConnection));
        }

        public static void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<MainContext>();
        }
    }
}
