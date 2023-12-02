using BookActivity.Shared.Extensions;
using BookActivity.Shared.Interfaces;
using LinqKit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BookActivity.Api.Common.Extensions
{
    public static class DIExtensions
    {
        public static void ConfigureModules(this IServiceCollection services, IConfiguration configuration)
        {
            LoadAssemblies();

            var moduleConfigurationType = typeof(IModuleConfiguration);
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => moduleConfigurationType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IModuleConfiguration>()
                .ForEach(m => m.ConfigureDI(services, configuration));
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "BookActivityServer",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSecretKey())),
                    ValidAudience = "BookActivityClient",
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
            });
        }

        //TODO: refactoring
        private static void LoadAssemblies()
        {
            try
            {
                Assembly.Load("BookActivity.Initialization");
            }
            catch
            {
                return;
            }

            Assembly.Load("BookActivity.Infrastructure.Data");
            Assembly.Load("BookActivity.Infrastructure");
        }
    }
}
