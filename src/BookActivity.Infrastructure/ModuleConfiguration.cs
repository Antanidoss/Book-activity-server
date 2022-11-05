using BookActivity.Domain.Interfaces;
using BookActivity.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;

namespace BookActivity.Infrastructure
{
    public sealed class ModuleConfiguration : IModuleConfiguration
    {
        public IServiceCollection ConfigureDI(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<IMediatorHandlerWithQuery, InMemoryBus>();

            return services;
        }
    }
}
