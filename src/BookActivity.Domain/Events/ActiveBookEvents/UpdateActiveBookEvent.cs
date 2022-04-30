using NetDevPack.Messaging;
using System;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public sealed class UpdateActiveBookEvent : Event
    {
        public UpdateActiveBookEvent(Guid activeBookId, int numberPagesRead)
        {
            ActiveBookId = activeBookId;
            NumberPagesRead = numberPagesRead;
        }

        public Guid ActiveBookId { get; private set; }
        public int NumberPagesRead { get; private set; }
    }
}