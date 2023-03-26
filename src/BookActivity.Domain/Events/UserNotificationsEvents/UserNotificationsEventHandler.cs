using BookActivity.Domain.Events.ActiveBookEvent;
using BookActivity.Domain.Events.AppUserEvents;
using BookActivity.Domain.Filters;
using BookActivity.Domain.Hubs;
using BookActivity.Domain.Interfaces.Hubs;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.AppUserSpecs;
using BookActivity.Domain.Specifications.BookSpecs;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Events.UserNotificationsEvents
{
    internal sealed class UserNotificationsEventHandler :
        INotificationHandler<AddActiveBookEvent>,
        INotificationHandler<SubscribeAppUserEvent>
    {
        private readonly IUserNotificationRepository _userNotificationRepository;

        private readonly IBookRepository _bookRepository;

        private readonly IUserNotificationsHub _userNotificationsHub;

        private readonly IAppUserRepository _appUserRepository;

        public UserNotificationsEventHandler(IBookRepository bookRepository, IUserNotificationsHub userNotificationsHub, IUserNotificationRepository userNotificationRepository, IAppUserRepository appUserRepository)
        {
            _bookRepository = bookRepository;
            _userNotificationsHub = userNotificationsHub;
            _userNotificationRepository = userNotificationRepository;
            _appUserRepository = appUserRepository;
        }

        public async Task Handle(AddActiveBookEvent notification, CancellationToken cancellationToken)
        {
            BookByIdSpec bookByIdSpec = new(notification.BookId);
            DbSingleResultFilterModel<Book> filterModel = new(bookByIdSpec, forUpdate: false);
            var book = await _bookRepository.GetByFilterAsync(filterModel);
            var user = await _appUserRepository.GetBySpecAsync(new AppUserByIdSpec(notification.UserId), true, u => u.Subscribers);

            string notificationMessage = $"{user.UserName} has made the book \"{book.Title}\" active";

            foreach (var followedUser in user.Subscribers)
            {
                _userNotificationRepository.Add(new UserNotification(notificationMessage, followedUser.UserIdWhoSubscribed));

                await _userNotificationsHub.Send(new UserNotificationModel(
                        Guid.Empty,
                        followedUser.UserIdWhoSubscribed,
                        notificationMessage
                    ));
            }
        }

        public async Task Handle(SubscribeAppUserEvent notification, CancellationToken cancellationToken)
        {
            string notificationMessage = $"{notification.UserNameWhoSubscribed} has subscribed to you";

            await _userNotificationsHub.Send(new UserNotificationModel(
                       Guid.Empty,
                       notification.SubscribedUserId,
                       notificationMessage
                   ));
        }
    }
}
