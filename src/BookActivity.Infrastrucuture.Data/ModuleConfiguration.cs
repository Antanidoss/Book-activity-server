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
using MongoDB.Driver.Linq;

namespace BookActivity.Infrastructure.Data
{
    public sealed class ModuleConfiguration : IModuleConfiguration
    {
        public IServiceCollection ConfigureDI(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookActivityContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    .EnableSensitiveDataLogging();
            });

            services.AddIdentity<AppUser, AppRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 6;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<BookActivityContext>().AddDefaultTokenProviders();

            ConfigureRepositories(services);
            ConfigureMongoDb(services, configuration);
            ConfigureGraphQL(services);

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

        private void ConfigureGraphQL(IServiceCollection services)
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

        private void ConfigureMongoDb(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("EventStoreConnection");
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.LinqProvider = LinqProvider.V3;

            services.AddSingleton(new MongoClient(settings).GetDatabase("BookActivityEvent"));

            ObjectSerializer objectSerializer = new(type => ObjectSerializer.DefaultAllowedTypes(type) || type.FullName.Contains(nameof(BookActivity)));
            BsonSerializer.RegisterSerializer(objectSerializer);

            GuidSerializer guidSerializer = new(BsonType.String);
            BsonSerializer.RegisterSerializer(guidSerializer);

            DateTimeSerializer dateTimeSerializer = new(BsonType.DateTime);
            BsonSerializer.RegisterSerializer(dateTimeSerializer);

            ConventionPack conventionPack = new() { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreExtraElements", conventionPack, type => true);
        }
    }
}
