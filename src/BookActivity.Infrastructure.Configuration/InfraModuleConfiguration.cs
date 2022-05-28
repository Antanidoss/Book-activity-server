using BookActivity.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookActivity.Infrastructure.Configuration
{
    public sealed class InfraModuleConfiguration : IModuleConfiguration
    {
        public IServiceCollection ConfigureDI(IServiceCollection services, IConfiguration Configuration)
        {
            var infraModeuleConfigure = new ModuleConfiguration();
            infraModeuleConfigure.ConfigureDI(services, Configuration);

            var infraDataModuleConfigure = new Data.ModuleConfiguration();
            infraDataModuleConfigure.ConfigureDI(services, Configuration);

            return services;
        }

        public void CreateDatabasesIfNotExist(IServiceScope serviceScope)
        {
            new Data.ModuleConfiguration().CreateDatabasesIfNotExist(serviceScope);
        }
    }
}
