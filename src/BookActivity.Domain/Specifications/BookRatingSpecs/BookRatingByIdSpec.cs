using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Models;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.BookRatingSpecs
{
    public sealed class BookRatingByIdSpec : ISpecification<BookRating>
    {
        private readonly Guid _bookRatingId;

        public BookRatingByIdSpec(Guid bookRatingId)
        {
            _bookRatingId = bookRatingId;
        }

        public Expression<Func<BookRating, bool>> ToExpression()
        {
            return r => r.Id == _bookRatingId;
        }
    }
}
