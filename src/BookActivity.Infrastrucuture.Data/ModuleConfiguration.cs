using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.Graphql;
using BookActivity.Infrastructure.Data.Intefaces;
using BookActivity.Infrastructure.Data.Repositories;
using BookActivity.Infrastructure.Data.Repositories.EventSourcing;
using BookActivity.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using MongoDB.Bson.Serialization.Conventions;

namespace BookActivity.Infrastructure.Data
{
    public sealed class ModuleConfiguration : IModuleConfiguration
    {
        public IServiceCollection ConfigureDI(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<BookActivityContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .EnableSensitiveDataLogging();
            });

            services.AddSingleton(new MongoClient(Configuration.GetConnectionString("EventStoreConnection")).GetDatabase("BookActivityEvent"));

            var objectSerializer = new ObjectSerializer(type => ObjectSerializer.DefaultAllowedTypes(type) || type.FullName.Contains("BookActivity"));
            BsonSerializer.RegisterSerializer(objectSerializer);
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreExtraElements", conventionPack, type => true);

            services.AddIdentity<AppUser, AppRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 6;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<BookActivityContext>().AddDefaultTokenProviders();

            ConfigureRepositories(services);

            AddGraphQL(services);

            return services;
        }

        public void CreateDatabasesIfNotExist(IServiceScope serviceScope)
        {
            CreateDatabasesIfNotExist(serviceScope.ServiceProvider.GetRequiredService<BookActivityContext>(), serviceScope.ServiceProvider);
        }

        private void CreateDatabasesIfNotExist(DbContext context, IServiceProvider serviceProvider)
        {
            if (context.Database.CanConnect())
                return;

            context.Database.EnsureCreated();

            if (context is BookActivityContext)
            {
                var initializer = serviceProvider.GetService<IDbInitializer>();
                if (initializer != null)
                {
                    initializer.InitializeAsync(context as BookActivityContext, serviceProvider.GetRequiredService<UserManager<AppUser>>()).GetAwaiter().GetResult();
                }
            }
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IActiveBookRepository, ActiveBookRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IBookNoteRepository, BookNoteRepository>();
            services.AddScoped<IBookRatingRepository, BookRatingRepositiory>();
            services.AddScoped<IBookOpinionRepository, BookOpinionRepository>();
            services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
            services.AddScoped<ISubscriberRepository, SubscriberRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        }

        private void AddGraphQL(IServiceCollection services)
        {
            services.AddGraphQLServer()
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
                .AddQueryType<Query>()
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .AddTypeExtension<BookExtensions>()
                .AddTypeExtension<UserExtensions>();
        }
    }
}
