using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public sealed class UpdateActiveBookEvent : Event
    {
        public int NumberPagesRead { get; private set; }
        public int CountPagesRead { get; private set; }
        public Guid BookId { get; private set; }

        public UpdateActiveBookEvent() { }

        public UpdateActiveBookEvent(Guid activeBookId, int newNumberPagesRead, int prevNumberPagesRead, Guid userId, Guid bookId) : base(userId, activeBookId)
        {
            NumberPagesRead = newNumberPagesRead;
            CountPagesRead = newNumberPagesRead - prevNumberPagesRead;
            BookId = bookId;
        }
    }
}