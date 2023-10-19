using BookActivity.Domain.Core.Events;
using System;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public class AddActiveBookEvent : Event
    {
        public Guid BookId { get; private set; }
        public int TotalNumberPages { get; private set; }
        public int NumberPagesRead { get; private set; }

        public AddActiveBookEvent(Guid activeBookId, int totalNumberPages, int numberPagesRead, Guid bookId, Guid userId) : base(userId, activeBookId)
        {
            BookId = bookId;
            TotalNumberPages = totalNumberPages;
            NumberPagesRead = numberPagesRead;
        }
    }
}