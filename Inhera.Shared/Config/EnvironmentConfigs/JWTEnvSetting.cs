namespace Inhera.Shared.Config.EnvironmentConfigs
{
    public class JWTEnvSetting
    {
        public required string Secret { get; set; }
        public string? ValidAudiences { get; set; }
        public string? ValidIssuer { get; set; }

    }
}
