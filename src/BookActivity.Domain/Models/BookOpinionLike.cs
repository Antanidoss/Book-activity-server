using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public sealed class BookOpinionLike : BaseEntity
    {
        public BookOpinion BookOpinion { get; private set; }
        public Guid BookId { get; private set; }
        public Guid UserIdOpinion { get; private set; }
        public AppUser UserWhoLike { get; private set; }
        public Guid UserIdWhoLike { get; private set; }

        public BookOpinionLike(Guid bookId, Guid userOpinionId, Guid userWhoLikeId)
        {
            BookId = bookId;
            UserIdOpinion = userOpinionId;
            UserIdWhoLike = userWhoLikeId;
        }

        //private, parameterless constructor used by EF Core
        private BookOpinionLike() { }
    }
}
