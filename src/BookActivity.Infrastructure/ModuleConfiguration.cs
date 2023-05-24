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

            return services;
        }

        public void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<UserNotificationsHub>("/userNotificationsHub");
        }

        private void ConfigureHubs(IServiceCollection services)
        {
            services.AddSingleton<IUserNotificationsHub, UserNotificationsHub>();
        }
    }
}
