using BookActivity.Domain.Interfaces.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BookActivity.Domain.Events.UserNotificationsEvents
{
    public sealed class UserNotificationsHub : Hub, IUserNotificationsHub
    {
        public async Task Send(UserNotificationModel notificationInfo)
        {
            await Clients.All.SendAsync(string.Empty, notificationInfo);
        }
    }
}
