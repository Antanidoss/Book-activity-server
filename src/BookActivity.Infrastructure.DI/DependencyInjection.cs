using BookActivity.Domain.Commands.ActiveBookCommands;
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BookActivityContext>(option => option.UseSqlServer(connection));
            services.AddDbContext<BookActivityEventStoreContext>(option => option.UseSqlServer(connection));

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

            services.AddScoped<IActiveBookRepository, ActiveBookRepository>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();

            services.AddScoped<IRequestHandler<AddActiveBookCommand, ValidationResult>, ActiveBookCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateActiveBookCommand, ValidationResult>, ActiveBookCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveActiveBookCommand, ValidationResult>, ActiveBookCommandHandler>();

            services.AddScoped<INotificationHandler<AddActiveBookEvent>, ActiveBookEventHandler>();
            services.AddScoped<INotificationHandler<UpdateActiveBookEvent>, ActiveBookEventHandler>();
            services.AddScoped<INotificationHandler<RemoveActiveBookEvent>, ActiveBookEventHandler>();
            services.AddScoped<INotificationHandler<AddActiveBookEvent>, UserNotificationsEventHandler>();

            return services;
        }

        public static void CreateDatabasesIfNotExist(IServiceScope serviceScope)
        {
            CreateDatabasesIfNotExist(serviceScope.ServiceProvider.GetRequiredService<BookActivityContext>());
            CreateDatabasesIfNotExist(serviceScope.ServiceProvider.GetRequiredService<BookActivityEventStoreContext>());
        }

        private static void CreateDatabasesIfNotExist(DbContext context)
        {
            if ((context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}