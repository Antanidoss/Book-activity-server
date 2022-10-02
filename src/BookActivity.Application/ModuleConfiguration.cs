using AutoMapper;
using BookActivity.Application.AutoMapper;
using BookActivity.Application.Implementation.Services;
using BookActivity.Application.Interfaces.Services;
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
            services.AddScoped<IBookNoteService, BookNoteService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookRatingService, BookRatingService>();
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
            mapperConfigureExpression.AddProfile(new ActiveBookDtoProfile());
            mapperConfigureExpression.AddProfile(new BookDtoProfile());
            mapperConfigureExpression.AddProfile(new AppUserDtoProfile());
            mapperConfigureExpression.AddProfile(new BookNoteDtoProfile());
            mapperConfigureExpression.AddProfile(new AuthorDtoProfile());
            mapperConfigureExpression.AddProfile(new BookOpinionDtoProfile());
            mapperConfigureExpression.AddProfile(new BookRatingDtoProfile());
        }
    }
}
