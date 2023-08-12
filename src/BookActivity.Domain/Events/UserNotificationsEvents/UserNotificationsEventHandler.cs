using BookActivity.Domain.Events.ActiveBookEvents;
using BookActivity.Domain.Events.AppUserEvents;
using BookActivity.Domain.Filters;
using BookActivity.Domain.Interfaces.Hubs;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Models.Notifications;
using BookActivity.Domain.Specifications.AppUserSpecs;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Events.UserNotificationsEvents
{
    public sealed class UserNotificationsEventHandler :
        INotificationHandler<AddActiveBookAfterOperationEvent>,
        INotificationHandler<SubscribeAppUserEvent>
    {
        private readonly IUserNotificationRepository _userNotificationRepository;

        private readonly IUserNotificationsHub _userNotificationsHub;

        private readonly IAppUserRepository _appUserRepository;

        public UserNotificationsEventHandler(IUserNotificationsHub userNotificationsHub, IUserNotificationRepository userNotificationRepository, IAppUserRepository appUserRepository)
        {
            _userNotificationsHub = userNotificationsHub;
            _userNotificationRepository = userNotificationRepository;
            _appUserRepository = appUserRepository;
        }

        public async Task Handle(AddActiveBookAfterOperationEvent notification, CancellationToken cancellationToken)
        {
            AppUserByIdSpec userByIdSpec = new(notification.UserId.Value);
            DbSingleResultFilterModel<AppUser> filterModelForUser = new(userByIdSpec, forUpdate: true, u => u.Subscribers);
            var user = await _appUserRepository.GetByFilterAsync(filterModelForUser);

            string notificationMessage = $"{user.UserName} has made the book id \"{notification.BookId}\" active";

            foreach (var followedUser in user.Subscribers)
            {
                UserNotification userNotification = new(notificationMessage, followedUser.UserIdWhoSubscribed) { Id = Guid.NewGuid() };
                _userNotificationRepository.Add(userNotification);

                await _userNotificationsHub.Send(new UserNotificationModel(
                        userNotification.Id,
                        followedUser.UserIdWhoSubscribed,
                        notificationMessage
                    ), followedUser.UserIdWhoSubscribed);
            }
        }

        public async Task Handle(SubscribeAppUserEvent notification, CancellationToken cancellationToken)
        {
            string notificationMessage = $"{notification.UserNameWhoSubscribed} has subscribed to you";

            UserNotification userNotification = new(notificationMessage, notification.SubscribedUserId) { Id = Guid.NewGuid() };
            _userNotificationRepository.Add(userNotification);

            await _userNotificationsHub.Send(new UserNotificationModel(
                       userNotification.Id,
                       notification.SubscribedUserId,
                       notificationMessage
                   ), notification.SubscribedUserId);
        }
    }
}
