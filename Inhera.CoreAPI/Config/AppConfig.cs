using Inhera.CoreAPI.Data;
using Inhera.CoreAPI.ResponderServices;
using Inhera.CoreAPI.Services;
using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Models.Database.SQL.Entities;
using Inhera.Shared.Repositories;
using Inhera.Shared.Util.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using System.Numerics;


namespace Inhera.CoreAPI.Config
{
    public static class AppConfig
    {
        //private const string OutputTemplate = "{Timestamp:dd-MM-yyyy HH:mm:ss} [{Level:u3}] [{ThreadId}] {Message}{NewLine}{Exception}";
        public static void Init(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<AppEnvironmentSetting>();
            var configuration = new AppEnvironmentSetting(builder.Environment);


            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                //.Enrich.WithThreadId()
                //.Enrich.WithProperty("Source", GetType().ToString())                
                //.WriteTo.Console(outputTemplate: OutputTemplate)
                .WriteTo.GrafanaLoki(
                    configuration.LoggerEnvironmentSetting.LokiBaseUrl,
                    new List<LokiLabel> { new() { Key = "app", Value = "core api service" } },
                    credentials: null
                    )
                    .CreateLogger();

            //logger
            builder.Services.AddScoped<ConsoleLogger, ConsoleLogger>();

            Serilog.ILogger logger = new LoggerConfiguration()
                    .WriteTo.GrafanaLoki(
                    "http://localhost:3100")
                    .CreateLogger();

            builder.Services.AddScoped<Serilog.ILogger>(e => logger);



            RegisterServices(builder);

        }

        public static void RegisterServices(WebApplicationBuilder builder)
        {
            var PostgresConnection = builder.Configuration.GetConnectionString("PostgresConnection");
            builder.Services.AddDbContext<CoreContext>(options =>
            options.UseNpgsql(PostgresConnection));

            builder.Services.AddScoped<CoreContext>();

            builder.Services.AddScoped<SqlRepository<UserProfileEntity, CoreContext>, SqlRepository<UserProfileEntity, CoreContext>>();
            builder.Services.AddScoped<SqlRepository<PlanEntity, CoreContext>, SqlRepository<PlanEntity, CoreContext>>();
            builder.Services.AddScoped<SqlRepository<LabCenterEntity, CoreContext>, SqlRepository<LabCenterEntity, CoreContext>>();
            builder.Services.AddScoped<SqlRepository<SubscriptionEntity, CoreContext>, SqlRepository<SubscriptionEntity, CoreContext>>();
            builder.Services.AddScoped<SqlRepository<AdditionalServiceEntity, CoreContext>, SqlRepository<AdditionalServiceEntity, CoreContext>>();
            builder.Services.AddScoped<SqlRepository<PaymentEntity, CoreContext>, SqlRepository<PaymentEntity, CoreContext>>();
            builder.Services.AddScoped<SqlRepository<LabCenterCalendarEntity, CoreContext>, SqlRepository<LabCenterCalendarEntity, CoreContext>>();
            builder.Services.AddScoped<SqlRepository<PaymentAttemptEntity, CoreContext>, SqlRepository<PaymentAttemptEntity, CoreContext>>();


            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddScoped<ProfileService>();
            builder.Services.AddScoped<PlanService>();
            builder.Services.AddScoped<SubscriptionService>();
            builder.Services.AddScoped<AdditionalServiceService>();
            builder.Services.AddScoped<SubscriptionService>();
            builder.Services.AddScoped<AdditionalServiceService>();
            builder.Services.AddScoped<LabCenterService>();
            builder.Services.AddScoped<ConfigurationService>();
            builder.Services.AddScoped<StripePaymentService>();
            builder.Services.AddScoped<PaymentService>();
            builder.Services.AddScoped<LabCenterCalendarService>();


            builder.Services.AddScoped<AuthResponderService>();
            builder.Services.AddScoped<ProfileResponderService>();
            builder.Services.AddScoped<PlanResponderService>();
            builder.Services.AddScoped<LabCenterResponderService>();
            builder.Services.AddScoped<ConfigurationResponderService>();
            builder.Services.AddScoped<PaymentResponderService>();
            builder.Services.AddScoped<LabCenterCalendarResponderService>();
            builder.Services.AddScoped<AdditionalServiceResponderService>();

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
                                      .AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
        }

        public static void AddCAPSupport(WebApplicationBuilder builder)
        {
            var configuration = new AppEnvironmentSetting(builder.Environment);
            builder.Services.AddCap(x =>
            {
                x.UseEntityFramework<CoreContext>(); // outbox/inbox
                x.UseRabbitMQ(opt =>
                {
                    opt.HostName = configuration.RabbitMQEnvSetting.HostName;
                    opt.UserName = configuration.RabbitMQEnvSetting.Password;
                    opt.Password = configuration.RabbitMQEnvSetting.Username;
                    opt.Port = int.Parse(configuration.RabbitMQEnvSetting.Port);
                    opt.ExchangeName = configuration.RabbitMQEnvSetting.Exchange;
                });
                x.UseDashboard();
                x.FailedRetryCount = 5;
                x.FailedRetryInterval = 60;       // seconds
            });

        }

        public static void AddSwaggerSupport(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });

            });
        }
    }

}
