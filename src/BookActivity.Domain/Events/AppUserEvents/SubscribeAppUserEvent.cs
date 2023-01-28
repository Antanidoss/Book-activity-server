using BookActivity.Domain.Core.Events;
using System;

namespace BookActivity.Domain.Events.AppUserEvents
{
    public sealed class SubscribeAppUserEvent : Event
    {
        public Guid SubscribedUserId { get; set; }
        public string UserNameWhoSubscribed { get; set; }
    }
}
