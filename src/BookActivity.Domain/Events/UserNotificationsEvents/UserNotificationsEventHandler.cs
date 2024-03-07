using BookActivity.Domain.Events.ActiveBookEvents;
using BookActivity.Domain.Events.AppUserEvents;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Interfaces.Hubs;
using BookActivity.Domain.Models;
using BookActivity.Domain.Models.Notifications;
using BookActivity.Domain.Specifications.AppUserSpecs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Events.UserNotificationsEvents
{
    public sealed class UserNotificationsEventHandler :
        INotificationHandler<AddActiveBookAfterOperationEvent>,
        INotificationHandler<SubscribeAppUserEvent>
    {
        private readonly IUserNotificationsHub _userNotificationsHub;
        private readonly IDbContext _efContext;

        public UserNotificationsEventHandler(IUserNotificationsHub userNotificationsHub, IDbContext efContext)
        {
            _userNotificationsHub = userNotificationsHub;
            _efContext = efContext;
        }

        public async Task Handle(AddActiveBookAfterOperationEvent notification, CancellationToken cancellationToken)
        {
            AppUserByIdSpec userByIdSpec = new(notification.UserId.Value);
            var user = await _efContext.Users
                .Where(userByIdSpec)
                .Include(u => u.Subscribers)
                .Select(u => new { u.Id, u.AvatarImage, u.Subscribers, u.UserName })
                .FirstAsync();

            string notificationMessage = $"{user.UserName} has made the book id \"{notification.BookId}\" active";

            foreach (var followedUser in user.Subscribers)
            {
                Notification userNotification = new(notificationMessage, followedUser.UserIdWhoSubscribed, user.Id) { Id = Guid.NewGuid() };
                await _efContext.Notifications.AddAsync(userNotification);

                await _userNotificationsHub.SendAsync(new UserNotificationModel(
                        userNotification.Id,
                        followedUser.UserIdWhoSubscribed,
                        notificationMessage,
                        Convert.ToBase64String(user.AvatarImage)
                    ));
            }

            await _efContext.Commit();
        }

        public async Task Handle(SubscribeAppUserEvent notification, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string notificationMessage = $"{notification.UserNameWhoSubscribed} has subscribed to you";

            Notification userNotification = new(notificationMessage, notification.SubscribedUserId, notification.UserIdWhoSubscribed) { Id = Guid.NewGuid() };
            await _efContext.Notifications.AddAsync(userNotification);

            AppUserByIdSpec userByIdSpec = new(notification.UserIdWhoSubscribed);
            var avatarImage = await _efContext.Users.Where(userByIdSpec).Select(u => u.AvatarImage).FirstAsync();

            await _userNotificationsHub.SendAsync(new UserNotificationModel(
                       userNotification.Id,
                       notification.SubscribedUserId,
                       notificationMessage,
                       Convert.ToBase64String(avatarImage)
                   ));

            await _efContext.Commit();
        }
    }
}
