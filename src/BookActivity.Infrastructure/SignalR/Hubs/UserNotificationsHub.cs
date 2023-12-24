using BookActivity.Domain.Interfaces.Hubs;
using BookActivity.Domain.Models.Notifications;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.SignalR.Hubs
{
    internal sealed class UserNotificationsHub : BaseHub, IUserNotificationsHub
    {
        private readonly IHubContext<UserNotificationsHub> _context;

        public UserNotificationsHub(IHubContext<UserNotificationsHub> context)
        {
            _context = context;
        }

        public async Task SendAsync(UserNotificationModel notificationInfo)
        {
            var connectionIds = GetConnectionIdsByUserId(notificationInfo.ToUserId);

            if (connectionIds == null || !connectionIds.Any())
                return;

            await _context.Clients
                .Clients(connectionIds)
                .SendAsync("GetUserNotification", JsonConvert.SerializeObject(notificationInfo));
        }
    }
}
