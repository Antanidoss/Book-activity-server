using System;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Hubs
{
    public interface IUserNotificationsHub
    {
        Task Send<T>(T notificationInfo, Guid userId);
    }
}
