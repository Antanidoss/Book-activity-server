using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Interfaces.Hubs;
using BookActivity.Infrastructure.SignalR.Hubs;
using BookActivity.Shared.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookActivity.Infrastructure
{
    public sealed class ModuleConfiguration : IModuleConfiguration
    {
        public IServiceCollection ConfigureDI(IServiceCollection services, IConfiguration Configuration)
        {
            ConfigureHubs(services);

            services.AddScoped<IMediatorHandler, InMemoryBus>();

            return services;
        }

        public static void ConfigureSignalREndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<UserNotificationsHub>("/userNotificationsHub");
        }

        private void ConfigureHubs(IServiceCollection services)
        {
            services.AddSingleton<IUserNotificationsHub, UserNotificationsHub>();
        }
    }
}
