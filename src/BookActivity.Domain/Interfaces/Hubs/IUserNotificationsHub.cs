using BookActivity.Domain.Models.Notifications;
using System;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Hubs
{
    public interface IUserNotificationsHub
    {
        Task SendAsync(UserNotificationModel notificationInfo);
    }
}
