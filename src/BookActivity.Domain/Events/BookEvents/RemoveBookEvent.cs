using NetDevPack.Messaging;
using System;

namespace BookActivity.Domain.Events.BookEvents
{
    public class RemoveBookEvent : Event
    {
        public Guid BookId { get; private set; }

        public RemoveBookEvent(Guid bookId)
        {
            BookId = bookId;
        }

    }
}
