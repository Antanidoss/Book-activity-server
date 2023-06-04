using BookActivity.Domain.Interfaces.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
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

        public async Task Send<T>(T notificationInfo, Guid userId)
        {
            var connectionIds = GetConnectionIdsByUserId(userId);

            if (connectionIds == null || !connectionIds.Any())
                return;

            await _context.Clients
                .Clients(connectionIds)
                .SendAsync("GetNotification", JsonConvert.SerializeObject(notificationInfo));
        }
    }
}
