using Inhera.NotificationService.Data;
using Inhera.NotificationService.EventHandlers;
using Inhera.NotificationService.Services;
using Microsoft.EntityFrameworkCore;
using System;
using static Inhera.NotificationService.Services.ViewRendererService;

namespace Inhera.NotificationService.Config
{
    public static class AppConfig
    {
        public static void Init(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<AppEnvironmentSetting>();
            //var configuration = new AppEnvironmentSetting(builder.Environment);


            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    //.Enrich.WithThreadId()
            //    //.Enrich.WithProperty("Source", GetType().ToString())                
            //    //.WriteTo.Console(outputTemplate: OutputTemplate)
            //    .WriteTo.GrafanaLoki(
            //        configuration.LoggerEnvironmentSetting.LokiBaseUrl,
            //        new List<LokiLabel> { new() { Key = "app", Value = "core api service" } },
            //        credentials: null
            //        )
            //        .CreateLogger();

            ////logger
            //builder.Services.AddScoped<ConsoleLogger, ConsoleLogger>();

            //Serilog.ILogger logger = new LoggerConfiguration()
            //        .WriteTo.GrafanaLoki(
            //        "http://localhost:3100")
            //        .CreateLogger();

            //builder.Services.AddScoped<Serilog.ILogger>(e => logger);

            var PostgresConnection = builder.Configuration.GetConnectionString("PostgresConnection");
            builder.Services.AddDbContext<MainContext>(options =>
            options.UseNpgsql(PostgresConnection));

            RegisterEventHandler(builder);

        }

        private static void RegisterEventHandler(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IViewRenderService, ViewRenderService>();
            builder.Services.AddScoped<EmailSenderService>();

            builder.Services.AddScoped<TestEventHandler>();
        }

        public static void AddCorsSupport(WebApplicationBuilder builder)
        {
            var DefaultCorsPolicy = "_DefaultCorsPolicy";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: DefaultCorsPolicy,
                                  policy =>
                                  {
                                      policy
                                      .WithOrigins("http://127.0.0.1:5500", "http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowCredentials()
                                      .AllowAnyMethod();
                                  });
            });
        }

        public static void AddCAPSupport(WebApplicationBuilder builder)
        {            
            var configuration = new AppEnvironmentSetting(builder.Environment);
            builder.Services.AddCap(x =>
            {
                x.UseEntityFramework<MainContext>();
                x.UseRabbitMQ(opt =>
                {
                    opt.HostName = configuration.RabbitMQEnvSetting.HostName;
                    opt.Port = int.Parse(configuration.RabbitMQEnvSetting.Port);
                    opt.UserName = configuration.RabbitMQEnvSetting.Username;
                    opt.Password = configuration.RabbitMQEnvSetting.Password;
                    opt.ExchangeName = configuration.RabbitMQEnvSetting.Exchange;
                });
                x.DefaultGroupName = "notification-service";
                x.FailedRetryCount = 5;
                x.FailedRetryInterval = 30;
            });
        }
    }
}
