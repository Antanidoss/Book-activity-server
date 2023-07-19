using BookActivity.Domain.Core.Events;
using System;

namespace BookActivity.Domain.Events.AppUserEvents
{
    public sealed class SubscribeAppUserEvent : Event
    {
        public Guid SubscribedUserId { get; init; }
        public string UserNameWhoSubscribed { get; set; }

        public SubscribeAppUserEvent(Guid subscribedUserId, string userNameWhoSubscribed) : base(subscribedUserId)
        {
            SubscribedUserId = subscribedUserId;
            UserNameWhoSubscribed = userNameWhoSubscribed;
        }
    }
}
