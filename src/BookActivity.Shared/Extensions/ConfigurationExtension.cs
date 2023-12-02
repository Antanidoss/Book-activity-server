using Microsoft.Extensions.Configuration;

namespace BookActivity.Shared.Extensions
{
    public static class ConfigurationExtension
    {
        public static string GetSecretKey(this IConfiguration configuration)
        {
            return configuration["TokenInfo:SecretKey"];
        }

        public static bool GetUseSqlLogs(this IConfiguration configuration)
        {
            return bool.Parse(configuration["UseSqlLogs"]);
        }
    }
}
