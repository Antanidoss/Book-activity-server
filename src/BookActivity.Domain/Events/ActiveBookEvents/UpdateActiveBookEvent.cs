using BookActivity.Domain.Core.Events;
using System;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public sealed class UpdateActiveBookEvent : Event
    {
        public int NumberPagesRead { get; private set; }

        public UpdateActiveBookEvent(Guid activeBookId, int numberPagesRead, Guid userId)
        {
            AggregateId = activeBookId;
            UserId = userId;
        }
    }
}