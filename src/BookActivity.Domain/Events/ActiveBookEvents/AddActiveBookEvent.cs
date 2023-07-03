using BookActivity.Domain.Core.Events;
using System;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public sealed class AddActiveBookEvent : Event
    {
        public readonly Guid BookId;

        public readonly int TotalNumberPages;

        public readonly int NumberPagesRead;

        public AddActiveBookEvent(Guid activeBookId, int totalNumberPages, int numberPagesRead, Guid bookId, Guid userId) : base(userId)
        {
            AggregateId = activeBookId;
            BookId = bookId;
            TotalNumberPages = totalNumberPages;
            NumberPagesRead = numberPagesRead;
        }
    }
}