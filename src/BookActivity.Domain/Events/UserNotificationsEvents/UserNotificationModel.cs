using System;

namespace BookActivity.Domain.Events.UserNotificationsEvents
{
    public sealed class UserNotificationModel
    {
        public readonly Guid InitiatingNotificationUserId;

        public readonly Guid OwnerNotificationUserId;

        public readonly string MessageNotification;

        public UserNotificationModel(Guid initiatingNotificationUserId, Guid ownerNotificationUserId, string messageNotification)
        {
            InitiatingNotificationUserId = initiatingNotificationUserId;
            OwnerNotificationUserId = ownerNotificationUserId;
            MessageNotification = messageNotification;
        }
    }
}
