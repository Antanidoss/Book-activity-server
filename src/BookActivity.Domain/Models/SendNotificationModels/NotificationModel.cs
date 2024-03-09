using System;

namespace BookActivity.Domain.Models.SendNotificationModels
{
    public class NotificationModel
    {
        public readonly Guid NotificationId;

        public readonly Guid ToUserId;

        public readonly string MessageNotification;

        public readonly Guid? FromUserId;

        public readonly string FromUserAvatarDataBase64;

        public NotificationModel(Guid notificationId, Guid toUserId, string messageNotification)
        {
            NotificationId = notificationId;
            ToUserId = toUserId;
            MessageNotification = messageNotification;
        }

        public NotificationModel(Guid notificationId, Guid toUserId, string messageNotification, Guid fromUserId, string fromUserAvatarDataBase64)
        {
            NotificationId = notificationId;
            ToUserId = toUserId;
            MessageNotification = messageNotification;
            FromUserId = fromUserId;
            FromUserAvatarDataBase64 = fromUserAvatarDataBase64;
        }
    }
}
