using BookActivity.Domain.Interfaces.Hubs;
using BookActivity.Domain.Models.SendNotificationModels;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.SignalR.Hubs
{
    internal sealed class NotificationsHub : BaseHub, INotificationsHub
    {
        private readonly IHubContext<NotificationsHub> _context;

        public NotificationsHub(IHubContext<NotificationsHub> context)
        {
            _context = context;
        }

        public async Task SendAsync(NotificationModel notificationInfo)
        {
            var connectionIds = GetConnectionIdsByUserId(notificationInfo.ToUserId);

            if (connectionIds == null || !connectionIds.Any())
                return;

            await _context.Clients
                .Clients(connectionIds)
                .SendAsync("GetNotification", JsonConvert.SerializeObject(notificationInfo));
        }
    }
}
