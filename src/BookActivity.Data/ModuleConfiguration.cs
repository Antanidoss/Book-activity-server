using BookActivity.Domain.Core.Events;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.EventSourcing;
using BookActivity.Infrastructure.Data.Repositories;
using BookActivity.Infrastructure.Data.Repositories.EventSourcing;
using BookActivity.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace BookActivity.Infrastructure.Data
{
    public sealed class ModuleConfiguration : IModuleConfiguration
    {
        public IServiceCollection ConfigureDI(IServiceCollection services, IConfiguration Configuration)
        {
            string deffaultConnection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BookActivityContext>(option => option.UseSqlServer(deffaultConnection).LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging());

            string eventStoreConnection = Configuration.GetConnectionString("EventStoreConnection");
            services.AddDbContext<BookActivityEventStoreContext>(option => option.UseSqlServer(eventStoreConnection));

            services.AddIdentity<AppUser, AppRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 6;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<BookActivityContext>().AddDefaultTokenProviders();

            services.AddScoped<IEventStore, EventStore>();

            ConfigureRepositories(services);

            return services;
        }

        public void CreateDatabasesIfNotExist(IServiceScope serviceScope)
        {
            CreateDatabasesIfNotExist(serviceScope.ServiceProvider.GetRequiredService<BookActivityContext>());
            CreateDatabasesIfNotExist(serviceScope.ServiceProvider.GetRequiredService<BookActivityEventStoreContext>());
        }

        private void CreateDatabasesIfNotExist(DbContext context)
        {
            if (!(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                context.Database.EnsureCreated();
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
    }
}
