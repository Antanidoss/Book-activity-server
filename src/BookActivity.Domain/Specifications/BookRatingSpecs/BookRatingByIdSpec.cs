using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Models;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.BookRatingSpecs
{
    public sealed class BookRatingByIdSpec : Specification<BookRating>
    {
        private readonly Guid _bookRatingId;

        public BookRatingByIdSpec(Guid bookRatingId)
        {
            _bookRatingId = bookRatingId;
        }

        public override Expression<Func<BookRating, bool>> ToExpression()
        {
            return r => r.Id == _bookRatingId;
        }
    }
}
