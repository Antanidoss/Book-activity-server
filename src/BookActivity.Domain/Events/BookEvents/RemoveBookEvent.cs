using BookActivity.Domain.Core.Events;
using System;

namespace BookActivity.Domain.Events.BookEvents
{
    public class RemoveBookEvent : Event
    {
        public RemoveBookEvent(Guid bookId, Guid userId)
        {
            AggregateId = bookId;
            UserId = userId;
        }
    }
}
