using BookActivity.Infrastructure.Data.Intefaces;
using BookActivity.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BookActivity.Api.Common.Extensions
{
    public static class DIExtensions
    {
        public static void ConfigureModules(this IServiceCollection services, IConfiguration configuration)
        {
            LoadCommonAssemblies();

            var typesModules = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes());

            ConfigureDbInitialization(services, typesModules);

            var moduleConfigurationType = typeof(IModuleConfiguration);
            var modules = typesModules.Where(t => moduleConfigurationType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IModuleConfiguration>();

            LoadCommonAssemblies();

            foreach (var module in modules)
                module.ConfigureDI(services, configuration);
        }

        private static void ConfigureDbInitialization(IServiceCollection services, IEnumerable<Type> typesModules)
        {
            try
            {
                Assembly.Load("BookActivity.Initialization");
            }
            catch
            {
                return;
            }

            Type dbInitializer = null;
            if ((dbInitializer = typesModules.FirstOrDefault(m => m.Name == "DbInitializer")) != null)
                services.AddSingleton(() => Activator.CreateInstance(dbInitializer) as IDbInitializer);
        }

        private static void LoadCommonAssemblies()
        {
            Assembly.Load("BookActivity.Infrastructure.Data");
            Assembly.Load("BookActivity.Infrastructure");
        }
    }
}
