using Inhera.Shared.Config.EnvironmentConfigs;
using Inhera.Shared.Models.Common;

namespace Inhera.CoreAPI.Config
{
    public class AppEnvironmentSetting
    {
        public MongoDBEnvSetting MongoDBSettings { get; private set; }
        public PostgresDBEnvSetting PostgresDBSettings { get; private set; }
        public JWTEnvSetting JWTEnvSetting { get; private set; }
        public string NotificationServiceBaseUrl { get; private set; }
        public LoggerEnvironmentSetting LoggerEnvironmentSetting { get; private set; }
        public string StripePublishableKey { get; private set; }
        public string StripeSecretKey { get; private set; }
        public RabbitMQEnvSetting RabbitMQEnvSetting { get; private set; }


        public AppEnvironmentSetting(IWebHostEnvironment env)
        {
            var confirguration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables()
            .Build();
            configureSettings(confirguration);
        }

        private void configureSettings(IConfiguration configuration)
        {
            MongoDBSettings = new()
            {
                //MongoDBSettings__URI
                URI = configuration.GetValue("MongoDBSettings:URI", ""),
                //MongoDBSettings__DatabaseName
                DatabaseName = configuration.GetValue("MongoDBSettings:DatabaseName", ""),
            };

            PostgresDBSettings = new()
            {
                //ConnectionStrings__PostgresConnection
                ConnectionString = configuration.GetValue("ConnectionStrings:PostgresConnection", ""),
            };

            JWTEnvSetting = new()
            {
                //JwtConfig__Secret
                Secret = configuration.GetValue("JwtConfig:Secret", ""),
                //JwtConfig__ValidAudiences
                ValidAudiences = configuration.GetValue("JwtConfig:ValidAudiences", ""),
                //JwtConfig__ValidIssuer
                ValidIssuer = configuration.GetValue("JwtConfig:ValidIssuer", ""),
            };

            //NotificationService__BaseUrl    => e.g. .../api/v1
            NotificationServiceBaseUrl = configuration.GetValue("NotificationService:BaseUrl", "");

            //Loki__BaseUrl
            LoggerEnvironmentSetting = new()
            {
                LokiBaseUrl = configuration.GetValue("Loki:BaseUrl", "")
            };

            //Stripe__Publishable_Key
            StripePublishableKey = configuration.GetValue("Stripe:PublishableKey", "");

            //Stripe__Secret_Key
            StripeSecretKey = configuration.GetValue("Stripe:SecretKey", "");

            //RabbitMQ__Host
            RabbitMQEnvSetting = new()
            {
                HostName = configuration.GetValue("RabbitMQ:Host", "localhost"),
                Port = configuration.GetValue("RabbitMQ:Port", "5672"),
                Username = configuration.GetValue("RabbitMQ:UserName", "guest"),
                Password = configuration.GetValue("RabbitMQ:Password", "guest"),
                Exchange = configuration.GetValue("RabbitMQ:Exchange", "notification.exchange")
            };
        }
    }
}