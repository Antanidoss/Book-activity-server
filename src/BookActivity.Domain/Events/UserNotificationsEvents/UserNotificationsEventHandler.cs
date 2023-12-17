using BookActivity.Domain.Events.ActiveBookEvents;
using BookActivity.Domain.Events.AppUserEvents;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Hubs;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Models.Notifications;
using BookActivity.Domain.Specifications.AppUserSpecs;
using MediatR;
using System;
using System.Linq.Expressions;
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
            var user = await GetUserByIdAsync(notification.UserId.Value, cancellationToken, u => u.Subscribers);

            string notificationMessage = $"{user.UserName} has made the book id \"{notification.BookId}\" active";

            foreach (var followedUser in user.Subscribers)
            {
                UserNotification userNotification = new(notificationMessage, followedUser.UserIdWhoSubscribed, user.Id) { Id = Guid.NewGuid() };
                await _userNotificationRepository.AddAsync(userNotification);

                await _userNotificationsHub.SendAsync(new UserNotificationModel(
                        userNotification.Id,
                        followedUser.UserIdWhoSubscribed,
                        notificationMessage,
                        Convert.ToBase64String(user.AvatarImage)
                    ));
            }

            await _userNotificationRepository.UnitOfWork.Commit();
        }

        public async Task Handle(SubscribeAppUserEvent notification, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string notificationMessage = $"{notification.UserNameWhoSubscribed} has subscribed to you";

            UserNotification userNotification = new(notificationMessage, notification.SubscribedUserId, notification.UserIdWhoSubscribed) { Id = Guid.NewGuid() };
            await _userNotificationRepository.AddAsync(userNotification);

            var avatarDataBase64 = Convert.ToBase64String((await GetUserByIdAsync(notification.UserIdWhoSubscribed, cancellationToken)).AvatarImage);

            await _userNotificationsHub.SendAsync(new UserNotificationModel(
                       userNotification.Id,
                       notification.SubscribedUserId,
                       notificationMessage,
                       avatarDataBase64
                   ));

            await _userNotificationRepository.UnitOfWork.Commit();
        }

        //TODO: оптимизировать запрос
        private async Task<AppUser> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken, params Expression<Func<AppUser, object>>[] includes)
        {
            AppUserByIdSpec userByIdSpec = new(userId);
            DbSingleResultFilterModel<AppUser> filterModelForUser = new(userByIdSpec, forUpdate: false, includes);

            return await _appUserRepository.GetByFilterAsync(filterModelForUser, cancellationToken);
        }
    }
}
