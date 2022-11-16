using BookActivity.Domain.Core.Events;
using Newtonsoft.Json;
using System;

namespace BookActivity.Domain.Events.ActiveBookEvent
{
    public sealed class AddActiveBookEvent : StoredEvent
    {
        public Guid BookId { get; set; }

        public AddActiveBookEvent(Guid activeBookId, int totalNumberPages, int numberPagesRead, Guid bookId, Guid userId)
        {
            AggregateId = activeBookId;
            UserId = userId;
            BookId = bookId;
            Data = JsonConvert.SerializeObject(new { totalNumberPages, numberPagesRead, bookId });
        }
    }
}