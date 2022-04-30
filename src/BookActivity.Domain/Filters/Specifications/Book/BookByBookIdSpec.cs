using BookActivity.Domain.Interfaces.Filters;
using System;
using System.Linq;

namespace BookActivity.Models.Filters.Specifications.Book
{
    public sealed class BookByBookIdSpec : IQueryableSpecification<Domain.Models.Book>
    {
        private readonly Guid _bookId;

        public BookByBookIdSpec(Guid bookId)
        {
            _bookId = bookId;
        }

        public IQueryable<Domain.Models.Book> Apply(IQueryable<Domain.Models.Book> query) => query.Where(b => b.Id == _bookId);
    }
}
