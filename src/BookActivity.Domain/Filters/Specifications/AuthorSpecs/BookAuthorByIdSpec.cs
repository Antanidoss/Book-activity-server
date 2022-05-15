using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System;
using System.Linq;

namespace BookActivity.Domain.Filters.Specifications.AuthorSpecs
{
    public sealed class BookAuthorByIdSpec : IQueryableFilterSpec<BookAuthor, Guid[]>
    {
        public IQueryable<BookAuthor> ApplyFilter(IQueryable<BookAuthor> query, Guid[] authorIds)
        {
            return query.Where(a => authorIds.Contains(a.Id));
        }

    }
}
