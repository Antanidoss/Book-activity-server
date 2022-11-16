using BookActivity.Domain.Core.Events;
using Newtonsoft.Json;
using System;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public sealed class UpdateActiveBookEvent : StoredEvent
    {
        public int NumberPagesRead { get; private set; }

        public UpdateActiveBookEvent(Guid activeBookId, int numberPagesRead, Guid userId)
        {
            AggregateId = activeBookId;
            UserId = userId;
            Data = JsonConvert.SerializeObject(new { numberPagesRead });
        }
    }
}