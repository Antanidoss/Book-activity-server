using BookActivity.Domain.Models.SendNotificationModels;
using System;

namespace BookActivity.Domain.Models.Notifications
{
    public sealed class UserNotificationModel : NotificationModel
    {
        public readonly string AvatarDataBase64; 

        public UserNotificationModel(Guid notificationId, Guid toUserId, string messageNotification, string avatarDataBase64) : base(notificationId, toUserId, messageNotification)
        {
            AvatarDataBase64 = avatarDataBase64;
        }
    }
}
