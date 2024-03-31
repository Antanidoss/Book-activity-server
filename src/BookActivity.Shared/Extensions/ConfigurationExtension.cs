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

        public static string GetPostgresSqlConnectionString(this IConfiguration configuration)
        {
            return configuration["ConnectionStrings:PostgresSQL"];
        }

        public static string GetMsSqlConnectionString(this IConfiguration configuration)
        {
            return configuration["ConnectionStrings:PostgresSQL"];
        }

        public static string GetDbProviderName(this IConfiguration configuration)
        {
            return configuration["DbProvider"];
        }
    }
}
