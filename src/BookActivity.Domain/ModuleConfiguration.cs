using Ardalis.Result;
using BookActivity.Domain.Commands.ActiveBookCommands;
using BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook;
using BookActivity.Domain.Commands.ActiveBookCommands.RemoveActiveBook;
using BookActivity.Domain.Commands.ActiveBookCommands.UpdateActiveBook;
using BookActivity.Domain.Commands.AppUserCommands.AddAppUser;
using BookActivity.Domain.Commands.AppUserCommands.SubscribeAppUser;
using BookActivity.Domain.Commands.AppUserCommands.UnsubscribeAppUser;
using BookActivity.Domain.Commands.AppUserCommands.UpdateAppUser;
using BookActivity.Domain.Commands.AuthorCommands.AddAuthor;
using BookActivity.Domain.Commands.BookCommands.AddBook;
using BookActivity.Domain.Commands.BookCommands.RemoveBook;
using BookActivity.Domain.Commands.BookCommands.UpdateBook;
using BookActivity.Domain.Commands.BookNoteCommands.AddBookNote;
using BookActivity.Domain.Commands.BookRatingCommands.UpdateBookRating;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Events.AppUserEvents;
using BookActivity.Domain.Events.UserNotificationsEvents;
using BookActivity.Domain.Filters.FilterHandlers;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Filters.SelectFilterHandlers;
using BookActivity.Domain.Hubs;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Interfaces.Hubs;
using BookActivity.Domain.Models;
using BookActivity.Domain.Queries.ActiveBookQueries.GetActiveBookByFilter;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatistic;
using BookActivity.Domain.Queries.AppUserQueries.AuthenticationUser;
using BookActivity.Domain.Queries.AppUserQueries.GetUsersByFilter;
using BookActivity.Domain.Queries.BookQueries.GetBookByFilterQuery;
using BookActivity.Shared.Interfaces;
using BookActivity.Shared.Models;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace BookActivity.Domain
{
    public sealed class ModuleConfiguration : IModuleConfiguration
    {
        public IServiceCollection ConfigureDI(IServiceCollection services, IConfiguration Configuration)
        {
            ConfigureCommandHandlers(services);
            ConfigureQueryHandlers(services);
            ConfigureEventHandlers(services);
            ConfigureFilterHandlers(services);
            ConfigureHubs(services);

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
            services.AddScoped<IRequestHandler<UnsubscribeAppUserCommand, ValidationResult>, UnsubscribeAppUserCommandHandler>();

            services.AddScoped<IRequestHandler<AddBookNoteCommand, ValidationResult>, AddBookNoteCommandHandler>();

            services.AddScoped<IRequestHandler<AddAuthorCommand, ValidationResult>, AddAuthorCommandHandler>();

            services.AddScoped<IRequestHandler<UpdateBookRatingCommand, ValidationResult>, UpdateBookRatingCommandHandler>();

        }

        private void ConfigureQueryHandlers(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<GetBooksByFilterQuery, EntityListResult<SelectedBook>>, GetBooksByFilterQueryHandler>();

            services.AddScoped<IRequestHandler<GetActiveBookStatisticQuery, ActiveBooksStatistic>, GetActiveBookStatisticQueryHandler>();

            services.AddScoped<IRequestHandler<GetActiveBookByFilterQuery, EntityListResult<SelectedActiveBook>>, GetActiveBookByFilterQueryHandler>();

            services.AddScoped<IRequestHandler<GetUsersByFilterQuery, EntityListResult<SelectedAppUser>>, GetUsersByFilterQueryHandler>();
            services.AddScoped<IRequestHandler<AuthenticationUserQuery, Result<AuthenticationResult>>, AuthenticationUserQueryHandler>();
        }

        private void ConfigureFilterHandlers(IServiceCollection services)
        {
            services.AddScoped<IFilterHandler<Book, GetBooksByFilterQuery>, BookFilterHandler>();
            services.AddScoped<IFilterHandler<ActiveBook, GetActiveBookByFilterQuery>, ActiveBookFilterHandler>();
            services.AddScoped<IFilterHandler<AppUser, GetUsersByFilterQuery>, AppUserFilterHandler>();

            services.AddScoped<IFilterSelectHandler<Book, IEnumerable<SelectedBook>, GetBooksByFilterQuery>, BookSelectFilterHandler>();
            services.AddScoped<IFilterSelectHandler<ActiveBook, IEnumerable<SelectedActiveBook>, GetActiveBookByFilterQuery>, ActiveBookSelectFilterHandler>();
            services.AddScoped<IFilterSelectHandler<AppUser, IEnumerable<SelectedAppUser>, GetUsersByFilterQuery>, AppUserSelectFilterHandler>();
        }

        private void ConfigureEventHandlers(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<AddActiveBookEvent>, UserNotificationsEventHandler>();
            services.AddScoped<INotificationHandler<SubscribeAppUserEvent>, UserNotificationsEventHandler>();
        }

        private void ConfigureHubs(IServiceCollection services)
        {
            services.AddSingleton<IUserNotificationsHub, UserNotificationsHub>();
        }
    }
}
