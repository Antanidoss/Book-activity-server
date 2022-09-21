using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook;
using BookActivity.Domain.Commands.ActiveBookCommands.RemoveActiveBook;
using BookActivity.Domain.Commands.ActiveBookCommands.UpdateActiveBook;
using BookActivity.Domain.Commands.AppUserCommands.AddAppUser;
using BookActivity.Domain.Commands.AppUserCommands.SubscribeAppUser;
using BookActivity.Domain.Commands.AppUserCommands.UpdateAppUser;
using BookActivity.Domain.Commands.AuthorCommands.AddAuthor;
using BookActivity.Domain.Commands.BookCommands.AddBook;
using BookActivity.Domain.Commands.BookCommands.RemoveBook;
using BookActivity.Domain.Commands.BookCommands.UpdateBook;
using BookActivity.Domain.Commands.BookNoteCommands.AddBookNote;
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
            services.AddScoped<IRequestHandler<AddActiveBookCommand, ValidationResult>, AddActiveBookCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateActiveBookCommand, ValidationResult>, UpdateActiveBookCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveActiveBookCommand, ValidationResult>, RemoveActiveBookCommandHandler>();

            services.AddScoped<IRequestHandler<AddBookCommand, ValidationResult>, AddBookCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateBookCommand, ValidationResult>, UpdateBookCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveBookCommand, ValidationResult>, RemoveBookCommandHandler>();

            services.AddScoped<IRequestHandler<AddAppUserCommand, ValidationResult>, AddAppUserCommandHandler>();
            services.AddScoped<IRequestHandler<SubscribeAppUserCommand, ValidationResult>, SubscribeAppUserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateAppUserCommand, ValidationResult>, UpdateAppUserCommandHandler>();

            services.AddScoped<IRequestHandler<AddBookNoteCommand, ValidationResult>, AddBookNoteCommandHandler>();

            services.AddScoped<IRequestHandler<AddAuthorCommand, ValidationResult>, AddAuthorCommandHandler>();
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
