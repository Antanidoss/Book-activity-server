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

namespace BookActivity.Application.Configuration
{
    public sealed class ApplicationModuleConfiguration : IModuleConfiguration
    {
        public IServiceCollection ConfigureDI(IServiceCollection services, IConfiguration Configuration)
        {
            MapperConfigurationExpression mapperConfigureExpression = new();
            mapperConfigureExpression.AddProfile(new ActiveBookDTOProfile());
            mapperConfigureExpression.AddProfile(new BookDTOProfile());

            var mappingConfig = new MapperConfiguration(mapperConfigureExpression);

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            AddFilterHandlers(services);
            AddServices(services);

            return services;
        }

        private static void AddFilterHandlers(IServiceCollection services)
        {
            services.AddSingleton<IFilterHandler<ActiveBook, ActiveBookFilterModel>, ActiveBookFilterHandler>();
            services.AddSingleton<IFilterHandler<Book, BookFilterModel>, BookFilterHandler>();
            services.AddSingleton<IFilterHandler<BookAuthor, BookAuthorFilterModel>, BookAuthorFilterHandler>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IActiveBookService, ActiveBookService>();
            services.AddScoped<IBookService, BookService>();
        }
    }
}
