using BookActivity.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace BookActivity.Api.Common.Extensions
{
    public static class DIExtensions
    {
        public static void ConfigureModules(this IServiceCollection services, IConfiguration configuration)
        {
            LoadAssemblies();

            var moduleConfigurationType = typeof(IModuleConfiguration);
            var modules = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => moduleConfigurationType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IModuleConfiguration>();

            foreach (var module in modules)
                module.ConfigureDI(services, configuration);
        }

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
