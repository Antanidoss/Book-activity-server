using BookActivity.Domain.Events.UserNotificationsEvents;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Hubs
{
    public interface IUserNotificationsHub
    {
        Task Send(UserNotificationModel notificationInfo);
    }
}
