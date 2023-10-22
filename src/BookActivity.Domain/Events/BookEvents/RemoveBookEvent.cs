using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Events.BookEvents
{
    public class RemoveBookEvent : Event
    {
        public RemoveBookEvent(Guid bookId, Guid userId) : base(userId, bookId) { }
    }
}
