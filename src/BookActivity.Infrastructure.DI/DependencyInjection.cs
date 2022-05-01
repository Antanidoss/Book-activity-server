using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Commands.BookCommands;
using BookActivity.Domain.Core.Events;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Events.UserNotificationsEvents;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.EventSourcing;
using BookActivity.Infrastructure.Data.Repositories;
using BookActivity.Infrastructure.Data.Repositories.EventSourcing;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;

namespace BookActivity.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfastructure(this IServiceCollection services, IConfiguration Configuration)
        {
            string deffaultConnection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BookActivityContext>(option => option.UseSqlServer(deffaultConnection));

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

            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<IEventStore, EventStore>();

            ConfigureRepositories(services);
            ConfigureCommandHandlers(services);
            ConfigureEventHandlers(services);

            return services;
        }

        public static void CreateDatabasesIfNotExist(IServiceScope serviceScope)
        {
            CreateDatabasesIfNotExist(serviceScope.ServiceProvider.GetRequiredService<BookActivityContext>());
            CreateDatabasesIfNotExist(serviceScope.ServiceProvider.GetRequiredService<BookActivityEventStoreContext>());
        }

        private static void CreateDatabasesIfNotExist(DbContext context)
        {
            if (!(context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                context.Database.EnsureCreated();
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IActiveBookRepository, ActiveBookRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
        }

        private static void ConfigureCommandHandlers(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddActiveBookCommand, ValidationResult>, ActiveBookCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateActiveBookCommand, ValidationResult>, ActiveBookCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveActiveBookCommand, ValidationResult>, ActiveBookCommandHandler>();

            services.AddScoped<IRequestHandler<AddBookCommand, ValidationResult>, BookCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateBookCommand, ValidationResult>, BookCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveBookCommand, ValidationResult>, BookCommandHandler>();
        }

        private static void ConfigureEventHandlers(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<AddActiveBookEvent>, ActiveBookEventHandler>();
            services.AddScoped<INotificationHandler<UpdateActiveBookEvent>, ActiveBookEventHandler>();
            services.AddScoped<INotificationHandler<RemoveActiveBookEvent>, ActiveBookEventHandler>();
            services.AddScoped<INotificationHandler<AddActiveBookEvent>, UserNotificationsEventHandler>();
        }
    }
}