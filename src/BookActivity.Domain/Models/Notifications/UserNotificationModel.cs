using System;

namespace BookActivity.Domain.Models.Notifications
{
    internal sealed class UserNotificationModel
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
