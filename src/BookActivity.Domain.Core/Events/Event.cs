using System;

namespace BookActivity.Domain.Core.Events
{
    public class Event : NetDevPack.Messaging.Event
    {
        public Guid UserId { get; set; }
    }
}
