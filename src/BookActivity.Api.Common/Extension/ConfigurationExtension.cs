using Microsoft.Extensions.Configuration;

namespace BookActivity.Api.Common.Extension
{
    public static class ConfigurationExtension
    {
        public static string GetSecretKey(this IConfiguration configuration)
        {
            return configuration["TokenInfo:SecretKey"];
        }
    }
}
