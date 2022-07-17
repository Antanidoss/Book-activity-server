using NetDevPack.Messaging;
using System;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public sealed class UpdateActiveBookEvent : Event
    {
        public int NumberPagesRead { get; private set; }

        public UpdateActiveBookEvent(Guid activeBookId, int numberPagesRead)
        {
            AggregateId = activeBookId;
            NumberPagesRead = numberPagesRead;
        }
    }
}