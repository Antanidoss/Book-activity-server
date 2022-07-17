using NetDevPack.Messaging;
using System;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public sealed class AddActiveBookEvent : Event
    {
        public int TotalNumberPages { get; private set; }
        public int NumberPagesRead { get; private set; }
        public Guid BookId { get; private set; }
        public Guid UserId { get; private set; }
        public bool IsPublic { get; private set; }

        public AddActiveBookEvent(Guid activeBookId, int totalNumberPages, int numberPagesRead, Guid bookId, Guid userId, bool isPublic)
        {
            AggregateId = activeBookId;
            TotalNumberPages = totalNumberPages;
            NumberPagesRead = numberPagesRead;
            BookId = bookId;
            UserId = userId;
            IsPublic = isPublic;
        }
    }
}