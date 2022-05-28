using BookActivity.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookActivity.Domain.Configuration
{
    public sealed class ModuleConfiguration : IModuleConfiguration
    {
        public IServiceCollection ConfigureDI(IServiceCollection services, IConfiguration Configuration)
        {
            var domainModuleConfigure = new Domain.ModuleConfiguration();
            domainModuleConfigure.ConfigureDI(services, Configuration);

            return services;
        }
    }
}
