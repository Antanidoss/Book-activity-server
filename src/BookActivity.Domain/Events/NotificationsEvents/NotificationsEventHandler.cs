using BookActivity.Domain.Events.ActiveBookEvents;
using BookActivity.Domain.Events.AppUserEvents;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Interfaces.Hubs;
using BookActivity.Domain.Models;
using BookActivity.Domain.Models.SendNotificationModels;
using BookActivity.Domain.Specifications.AppUserSpecs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Events.NotificationsEvents
{
    public sealed class NotificationsEventHandler : NotificationHandler,
        INotificationHandler<AddActiveBookAfterOperationEvent>,
        INotificationHandler<SubscribeAppUserEvent>
    {
        private readonly INotificationsHub _notificationsHub;

        public NotificationsEventHandler(INotificationsHub userNotificationsHub, IDbContext dbContext) : base(dbContext)
        {
            _notificationsHub = userNotificationsHub;
        }

        public async Task Handle(AddActiveBookAfterOperationEvent notification, CancellationToken cancellationToken)
        {
            AppUserByIdSpec userByIdSpec = new(notification.UserId.Value);
            var user = await _dbContext.Users
                .Where(userByIdSpec)
                .Include(u => u.Subscribers)
                .Select(u => new { u.Id, u.AvatarImage, u.Subscribers, u.UserName })
                .FirstAsync();

            string notificationMessage = $"{user.UserName} has made the book id \"{notification.BookId}\" active";

            foreach (var followedUser in user.Subscribers)
            {
                Notification userNotification = new(notificationMessage, followedUser.UserIdWhoSubscribed, user.Id) { Id = Guid.NewGuid() };
                await _dbContext.Notifications.AddAsync(userNotification);

                await _notificationsHub.SendAsync(new NotificationModel(
                        userNotification.Id,
                        followedUser.UserIdWhoSubscribed,
                        notificationMessage,
                        followedUser.SubscribedUserId,
                        Convert.ToBase64String(user.AvatarImage)
                    ));
            }

            await Commit();
        }

        public async Task Handle(SubscribeAppUserEvent notification, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string notificationMessage = $"{notification.UserNameWhoSubscribed} has subscribed to you";

            Notification userNotification = new(notificationMessage, notification.SubscribedUserId, notification.UserIdWhoSubscribed) { Id = Guid.NewGuid() };
            await _dbContext.Notifications.AddAsync(userNotification);

            AppUserByIdSpec userByIdSpec = new(notification.UserIdWhoSubscribed);
            var avatarImage = await _dbContext.Users.Where(userByIdSpec).Select(u => u.AvatarImage).FirstAsync();

            await _notificationsHub.SendAsync(new NotificationModel(
                       userNotification.Id,
                       notification.SubscribedUserId,
                       notificationMessage,
                       notification.UserIdWhoSubscribed,
                       Convert.ToBase64String(avatarImage)
                   ));

            await Commit();
        }
    }
}
