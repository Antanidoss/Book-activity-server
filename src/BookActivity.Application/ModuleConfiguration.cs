using AutoMapper;
using BookActivity.Application.AutoMapper;
using BookActivity.Application.Implementation.FilterHandlers;
using BookActivity.Application.Implementation.Services;
using BookActivity.Application.Interfaces;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
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
            AddFilterHandlers(services);
            AddServices(services);

            return services;
        }

        private void AddAutoMapper(IServiceCollection services)
        {
            MapperConfigurationExpression mapperConfigureExpression = new();

            mapperConfigureExpression.AddProfile(new ActiveBookDTOProfile());
            mapperConfigureExpression.AddProfile(new BookDTOProfile());
            mapperConfigureExpression.AddProfile(new AppUserDTOProfile());

            MapperConfiguration mappingConfig = new(mapperConfigureExpression);

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void AddFilterHandlers(IServiceCollection services)
        {
            services.AddSingleton<IFilterHandler<ActiveBook, ActiveBookFilterModel>, ActiveBookFilterHandler>();
            services.AddSingleton<IFilterHandler<Book, BookFilterModel>, BookFilterHandler>();
            services.AddSingleton<IFilterHandler<BookAuthor, BookAuthorFilterModel>, BookAuthorFilterHandler>();
            services.AddSingleton<IFilterHandler<AppUser, AppUserFilterModel>, AppUserFilterHandler>();
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddScoped<IActiveBookService, ActiveBookService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAppUserService, AppUserService>();
        }
    }
}
