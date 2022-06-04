using BookActivity.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookActivity.Infrastructure.Configuration
{
    public sealed class InfraModuleConfiguration : IModuleConfiguration
    {
        public IServiceCollection ConfigureDI(IServiceCollection services, IConfiguration Configuration)
        {
            ModuleConfiguration infraModeuleConfigure = new();
            infraModeuleConfigure.ConfigureDI(services, Configuration);

            Data.ModuleConfiguration infraDataModuleConfigure = new();
            infraDataModuleConfigure.ConfigureDI(services, Configuration);

            Domain.ModuleConfiguration domainModuleConfigure = new();
            domainModuleConfigure.ConfigureDI(services, Configuration);

            return services;
        }

        public void CreateDatabasesIfNotExist(IServiceScope serviceScope)
        {
            new Data.ModuleConfiguration().CreateDatabasesIfNotExist(serviceScope);
        }
    }
}
