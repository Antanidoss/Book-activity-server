using BookActivity.Domain.Core.Events;
using System;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public sealed class RemoveActiveBookEvent : Event
    {
        public RemoveActiveBookEvent(Guid activeBookId)
        {
            AggregateId = activeBookId;
        }
    }
}