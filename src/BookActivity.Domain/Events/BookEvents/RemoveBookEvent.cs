using NetDevPack.Messaging;
using System;

namespace BookActivity.Domain.Events.BookEvents
{
    public class RemoveBookEvent : Event
    {
        public RemoveBookEvent(Guid bookId)
        {
            AggregateId = bookId;
        }

    }
}
