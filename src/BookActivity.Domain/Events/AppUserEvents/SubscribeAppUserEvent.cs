using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Events.AppUserEvents
{
    public sealed class SubscribeAppUserEvent : Event
    {
        public override WhenCallHandler WhenCallHandler { get; } = WhenCallHandler.AfterOperation;
        public Guid SubscribedUserId { get; init; }
        public Guid UserIdWhoSubscribed { get; init; }
        public string UserNameWhoSubscribed { get; set; }

        public SubscribeAppUserEvent(Guid subscribedUserId, Guid userIdWhoSubscribed, string userNameWhoSubscribed) : base(subscribedUserId)
        {
            SubscribedUserId = subscribedUserId;
            UserIdWhoSubscribed = userIdWhoSubscribed;
            UserNameWhoSubscribed = userNameWhoSubscribed;
        }
    }
}
