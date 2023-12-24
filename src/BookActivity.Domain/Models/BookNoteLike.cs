using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public sealed class BookNoteLike : BaseEntity
    {
        public BookNote BookNote { get; private set; }
        public Guid BookNoteId { get; private set; }
        public AppUser User { get; private set; }
        public Guid UserId { get; private set; }

        public BookNoteLike() : base() { }

        public BookNoteLike(Guid bookNoteId, Guid userId)
        {
            BookNoteId = bookNoteId;
            UserId = userId;
        }
    }
}
