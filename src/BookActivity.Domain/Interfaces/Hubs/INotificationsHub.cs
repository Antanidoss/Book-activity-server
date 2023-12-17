using BookActivity.Domain.Models.SendNotificationModels;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Hubs
{
    public interface INotificationsHub
    {
        Task SendAsync(NotificationModel notificationInfo);
    }
}
