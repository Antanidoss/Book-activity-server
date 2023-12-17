using System;

namespace BookActivity.Domain.Models.SendNotificationModels
{
    public class NotificationModel
    {
        public readonly Guid NotificationId;

        public readonly Guid ToUserId;

        public readonly string MessageNotification;

        public NotificationModel(Guid notificationId, Guid toUserId, string messageNotification)
        {
            NotificationId = notificationId;
            ToUserId = toUserId;
            MessageNotification = messageNotification;
        }
    }
}
