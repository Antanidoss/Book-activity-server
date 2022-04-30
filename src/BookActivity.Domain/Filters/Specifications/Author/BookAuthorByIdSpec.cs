using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System;
using System.Linq;

namespace BookActivity.Domain.Filters.Specifications.Author
{
    public sealed class BookAuthorByIdSpec : IQueryableSpecification<BookAuthor>
    {
        private readonly Guid _authorId;

        public BookAuthorByIdSpec(Guid authorId)
        {
            _authorId = authorId;
        }

        public IQueryable<BookAuthor> Apply(IQueryable<BookAuthor> query) => query.Where(a => a.Id == _authorId);
    }
}
