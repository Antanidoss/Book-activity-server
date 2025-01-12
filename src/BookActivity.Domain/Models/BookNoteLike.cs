using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public sealed class BookNoteLike : BaseEntity
    {
        public BookNote BookNote { get; private set; }
        public Guid BookNoteId { get; private set; }
        public AppUser UserWhoLike { get; private set; }
        public Guid UserIdWhoLike { get; private set; }

        public BookNoteLike(Guid bookNoteId, Guid userWhoLikeId)
        {
            BookNoteId = bookNoteId;
            UserIdWhoLike = userWhoLikeId;
        }

        //private, parameterless constructor used by EF Core
        private BookNoteLike() { }
    }
}
