using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public sealed class BookOpinionDislike : BaseEntity
    {
        public BookOpinion BookOpinion { get; private set; }
        public Guid BookId { get; private set; }
        public AppUser User { get; private set; }
        public Guid UserId { get; private set; }

        public BookOpinionDislike() : base() { }

        public BookOpinionDislike(Guid bookId, Guid userId)
        {
            BookId = bookId;
            UserId = userId;
        }
    }
}
