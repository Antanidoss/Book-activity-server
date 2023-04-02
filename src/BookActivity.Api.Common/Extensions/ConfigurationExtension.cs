using Microsoft.Extensions.Configuration;

namespace BookActivity.Api.Common.Extensions
{
    public static class ConfigurationExtension
    {
        public static string GetSecretKey(this IConfiguration configuration)
        {
            return configuration["TokenInfo:SecretKey"];
        }
    }
}
