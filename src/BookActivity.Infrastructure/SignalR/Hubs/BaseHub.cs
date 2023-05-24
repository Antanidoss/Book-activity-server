using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.SignalR.Hubs
{
    public class BaseHub : Hub
    {
        protected static readonly ConcurrentDictionary<string, Guid> ConnectionInfos = new();

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ConnectionInfos.Keys.Remove(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }

        public void SetUserInfo(string connectionId, Guid userId)
        {
            ConnectionInfos.TryAdd(connectionId, userId);
        }

        protected string[] GetConnectionIdsByUserId(Guid userId)
        {
            return ConnectionInfos
                .Where(i => i.Value == userId)
                .Select(i => i.Key)
                .ToArray();
        }
    }
}
