using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.AuthorSpecs
{
    public sealed class BookAuthorByIdSpec : ISpecification<BookAuthor>
    {
        private readonly Guid[] _authorIds;

        public BookAuthorByIdSpec(params Guid[] authorIds)
        {
            _authorIds = authorIds;
        }

        public Expression<Func<BookAuthor, bool>> ToExpression()
        {
            return a => _authorIds.Contains(a.Id);
        }
    }
}