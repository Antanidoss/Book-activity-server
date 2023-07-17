using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookActivity.Shared.Interfaces
{
    public interface IModuleConfiguration
    {
        IServiceCollection ConfigureDI(IServiceCollection services, IConfiguration Configuration);
    }
}
