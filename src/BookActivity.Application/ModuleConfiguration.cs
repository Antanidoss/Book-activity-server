using AutoMapper;
using BookActivity.Application.AutoMapper;
using BookActivity.Application.Implementation.Services;
using BookActivity.Application.Interfaces;
using BookActivity.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookActivity.Application
{
    public sealed class ModuleConfiguration : IModuleConfiguration
    {
        public IServiceCollection ConfigureDI(IServiceCollection services, IConfiguration Configuration)
        {
            AddAutoMapper(services);
            AddServices(services);

            return services;
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddScoped<IActiveBookService, ActiveBookService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAppUserService, AppUserService>();
        }

        private void AddAutoMapper(IServiceCollection services)
        {
            MapperConfigurationExpression mapperConfigureExpression = new();
            AddMapperProfiles(mapperConfigureExpression);
            MapperConfiguration mappingConfig = new(mapperConfigureExpression);
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void AddMapperProfiles(MapperConfigurationExpression mapperConfigureExpression)
        {
            mapperConfigureExpression.AddProfile(new ActiveBookDTOProfile());
            mapperConfigureExpression.AddProfile(new BookDTOProfile());
            mapperConfigureExpression.AddProfile(new AppUserDTOProfile());
        }
    }
}
