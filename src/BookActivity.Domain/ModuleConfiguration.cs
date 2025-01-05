using Ardalis.Result;
using BookActivity.Domain.Cache;
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
using BookActivity.Domain.Commands.BookOpinionCommads.AddBookOpinion;
using BookActivity.Domain.Commands.BookOpinionCommads.AddDislike;
using BookActivity.Domain.Commands.BookOpinionCommads.AddLike;
using BookActivity.Domain.Commands.BookOpinionCommads.RemoveDislike;
using BookActivity.Domain.Commands.BookOpinionCommads.RemoveLike;
using BookActivity.Domain.Commands.UserNotificationCommands.RemoveUserNotifications;
using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Events.ActiveBookEvents;
using BookActivity.Domain.Events.ActiveBookStatisticEvents;
using BookActivity.Domain.Events.AppUserEvents;
using BookActivity.Domain.Events.NotificationsEvents;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatistic;
using BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatisticByDay;
using BookActivity.Domain.Queries.AppUserQueries.AuthenticationUser;
using BookActivity.Domain.Queries.AppUserQueries.GetCurrentUser;
using BookActivity.Domain.Queries.OcrQueries.GetTextOnImage;
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
            ConfigureMemoryCache(services);

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

            services.AddScoped<IRequestHandler<AddBookOpinionCommand, ValidationResult>, AddBookOpinionCommandHandler>();
            services.AddScoped<IRequestHandler<AddDislikeCommand, ValidationResult>, AddDislikeCommandHandler>();
            services.AddScoped<IRequestHandler<AddLikeCommand, ValidationResult>, AddLikeCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveDislikeCommand, ValidationResult>, RemoveDislikeCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveLikeCommand, ValidationResult>, RemoveLikeCommandHandler>();

            services.AddScoped<IRequestHandler<RemoveUserNotificationsCommand, ValidationResult>, RemoveUserNotificationsCommandHandler>();
        }

        private void ConfigureQueryHandlers(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<GetActiveBookStatisticQuery, ActiveBooksStatistic>, GetActiveBookStatisticQueryHandler>();
            services.AddScoped<IRequestHandler<GetActiveBooksStatisticByDayQuery, IEnumerable<ActiveBookStatisticByDay>>, GetActiveBooksStatisticByDayQueryHandler>();

            services.AddScoped<IRequestHandler<AuthenticationUserQuery, Result<AuthenticationResult>>, AuthenticationUserQueryHandler>();
            services.AddScoped<IRequestHandler<GetCurrentUserQuery, CurrentUser>, GetCurrentUserQueryHandler>();

            services.AddScoped<IRequestHandler<GetTextOnImageQuery, string>, GetTextOnImageQueryHandler>();
        }

        private void ConfigureEventHandlers(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<AddActiveBookAfterOperationEvent>, NotificationsEventHandler>();
            services.AddScoped<INotificationHandler<SubscribeAppUserEvent>, NotificationsEventHandler>();

            services.AddScoped<INotificationHandler<AddActiveBookAfterOperationEvent>, ActiveBookStatisticEventHandler>();
            services.AddScoped<INotificationHandler<UpdateActiveBookEvent>, ActiveBookStatisticEventHandler>();
            services.AddScoped<INotificationHandler<RemoveActiveBookEvent>, ActiveBookStatisticEventHandler>();
        }

        private void ConfigureMemoryCache(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ActiveBookStatisticCache>();
            services.AddSingleton<UserCache>();
        }
    }
}
