using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Filters.Specifications.AuthorSpecs
{
    public sealed class BookAuthorByIdSpec : IQueryableFilterSpec<BookAuthor>
    {
        private readonly Guid[] _authorIds;

        public BookAuthorByIdSpec(params Guid[] authorIds)
        {
            _authorIds = authorIds;
        }

        public IQueryable<BookAuthor> ApplyFilter(IQueryable<BookAuthor> authors)
        {
            return authors.Where(ToExpression());
        }

        public Expression<Func<BookAuthor, bool>> ToExpression()
        {
            return a => _authorIds.Contains(a.Id);
        }
    }
}