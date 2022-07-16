using NetDevPack.Messaging;
using System;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public sealed class RemoveActiveBookEvent : Event
    {
        public Guid ActiveBookId { get; private set; }

        public RemoveActiveBookEvent(Guid activeBookId)
        {
            ActiveBookId = activeBookId;
            AggregateId = activeBookId;
        }

    }
}