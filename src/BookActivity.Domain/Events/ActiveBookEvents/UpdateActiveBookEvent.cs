using BookActivity.Domain.Core.Events;
using System;
using System.Text.Json.Serialization;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public sealed class UpdateActiveBookEvent : Event
    {
        [JsonInclude]
        public int NumberPagesRead { get; private set; }

        [JsonInclude]
        public int CountPagesRead { get; private set; }

        public UpdateActiveBookEvent() { }

        public UpdateActiveBookEvent(Guid activeBookId, int newNumberPagesRead, int prevNumberPagesRead, Guid userId) : base(userId)
        {
            AggregateId = activeBookId;
            NumberPagesRead = newNumberPagesRead;
            CountPagesRead = newNumberPagesRead - prevNumberPagesRead;
        }
    }
}