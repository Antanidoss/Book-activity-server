using BookActivity.Domain.Interfaces.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Domain.Hubs
{
    public sealed class UserNotificationsHub : BaseHub, IUserNotificationsHub
    {
        private readonly IHubContext<UserNotificationsHub> _context;

        public UserNotificationsHub(IHubContext<UserNotificationsHub> context)
        {
            _context = context;
        }

        public async Task Send(UserNotificationModel notificationInfo)
        {
            var connectionIds = GetConnectionIdsByUserId(notificationInfo.OwnerNotificationUserId);

            if (connectionIds == null || !connectionIds.Any())
                return;

            await _context.Clients
                .Clients(connectionIds)
                .SendAsync("GetNotification", JsonConvert.SerializeObject(notificationInfo));
        }
    }

    public sealed class UserNotificationModel
    {
        public readonly Guid NotificationId;

        public readonly Guid OwnerNotificationUserId;

        public readonly string MessageNotification;

        public UserNotificationModel(Guid notificationId, Guid ownerNotificationUserId, string messageNotification)
        {
            NotificationId = notificationId;
            OwnerNotificationUserId = ownerNotificationUserId;
            MessageNotification = messageNotification;
        }
    }
}
