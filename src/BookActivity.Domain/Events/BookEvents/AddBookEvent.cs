using BookActivity.Domain.Core;
using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Events.BookEvents
{
    public sealed class AddBookEvent : Event
    {
        public string Title { get; set; }
        public string Description { get; private set; }
        public IEnumerable<Guid> AuthorIds { get; private set; }

        public AddBookEvent(Guid bookId, string title, string description, IEnumerable<Guid> authorIds) : base(Guid.Empty, bookId)
        {
            Title = title;
            Description = description;
            AuthorIds = authorIds;
        }
    }
}
