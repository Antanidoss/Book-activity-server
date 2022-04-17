using AutoMapper;
using BookActivity.Application.AutoMapper;
using BookActivity.Application.Implementation;
using BookActivity.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookActivity.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            MapperConfigurationExpression mapperConfigureExpression = new();
            mapperConfigureExpression.AddProfile(new ActiveBookDTOProfile());
            mapperConfigureExpression.AddProfile(new BookDTOProfile());

            var mappingConfig = new MapperConfiguration(mapperConfigureExpression);

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IActiveBookService, ActiveBookService>();
            services.AddScoped<IBookService, BookService>();

            return services;
        }
    }
}