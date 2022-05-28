using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Commands.AppUserCommands;
using BookActivity.Domain.Commands.BookCommands;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Events.UserNotificationsEvents;
using BookActivity.Shared.Interfaces;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookActivity.Domain
{
    public sealed class ModuleConfiguration : IModuleConfiguration
    {
        public IServiceCollection ConfigureDI(IServiceCollection services, IConfiguration Configuration)
        {
            ConfigureCommandHandlers(services);
            ConfigureEventHandlers(services);

            return services;
        }

        private void ConfigureCommandHandlers(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddActiveBookCommand, ValidationResult>, ActiveBookCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateActiveBookCommand, ValidationResult>, ActiveBookCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveActiveBookCommand, ValidationResult>, ActiveBookCommandHandler>();

            services.AddScoped<IRequestHandler<AddBookCommand, ValidationResult>, BookCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateBookCommand, ValidationResult>, BookCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveBookCommand, ValidationResult>, BookCommandHandler>();

            services.AddScoped<IRequestHandler<AddAppUserCommand, ValidationResult>, AppUserCommandHandler>();
        }

        private void ConfigureEventHandlers(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<AddActiveBookEvent>, ActiveBookEventHandler>();
            services.AddScoped<INotificationHandler<UpdateActiveBookEvent>, ActiveBookEventHandler>();
            services.AddScoped<INotificationHandler<RemoveActiveBookEvent>, ActiveBookEventHandler>();
            services.AddScoped<INotificationHandler<AddActiveBookEvent>, UserNotificationsEventHandler>();
        }
    }
}
