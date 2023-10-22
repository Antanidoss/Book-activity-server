using BookActivity.Domain.Core;
using System;
using System.Text.Json.Serialization;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public sealed class UpdateActiveBookEvent : Event
    {
        public int NumberPagesRead { get; private set; }
        public int CountPagesRead { get; private set; }

        public UpdateActiveBookEvent() { }

        public UpdateActiveBookEvent(Guid activeBookId, int newNumberPagesRead, int prevNumberPagesRead, Guid userId) : base(userId, activeBookId)
        {
            NumberPagesRead = newNumberPagesRead;
            CountPagesRead = newNumberPagesRead - prevNumberPagesRead;
        }
    }
}