using BookActivity.Domain.Core.Events;
using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Events.BookEvents
{
    public class UpdateBookEvent : Event
    {
        public string Title { get; set; }
        public string Description { get; private set; }
        public IEnumerable<Guid> AuthorIds { get; private set; }
        public bool IsPublic { get; private set; }

        public UpdateBookEvent(Guid bookId, string title, string description, IEnumerable<Guid> authorIds, Guid userId) : base(userId)
        {
            AggregateId = bookId;
            Title = title;
            Description = description;
            AuthorIds = authorIds;
        }
    }
}
