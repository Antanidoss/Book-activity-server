using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public sealed class BookOpinionDislike : BaseEntity
    {
        public BookOpinion BookOpinion { get; private set; }
        public Guid BookId { get; private set; }
        public Guid UserIdOpinion { get; private set; }
        public AppUser UserWhoDislike { get; private set; }
        public Guid UserIdWhoDislike { get; private set; }

        public BookOpinionDislike(Guid bookId, Guid userIdOpinion, Guid userIdWhoDislike)
        {
            BookId = bookId;
            UserIdOpinion = userIdOpinion;
            UserIdWhoDislike = userIdWhoDislike;
        }

        //private, parameterless constructor used by EF Core
        private BookOpinionDislike() { }
    }
}
