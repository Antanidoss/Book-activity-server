using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.BookSpecs
{
    public sealed class BookByRatingRange : ISpecification<Book>
    {
        private readonly float _averageRatingFrom;

        private readonly float _averageRatingTo;

        public BookByRatingRange(float averageRatingFrom, float averageRatingTo)
        {
            _averageRatingFrom = averageRatingFrom;
            _averageRatingTo = averageRatingTo;
        }

        public Expression<Func<Book, bool>> ToExpression()
        {
            return b => _averageRatingFrom <= b.BookRating.BookOpinions.Average(o => o.Grade) && _averageRatingTo >= b.BookRating.BookOpinions.Average(o => o.Grade);
        }
    }
}
