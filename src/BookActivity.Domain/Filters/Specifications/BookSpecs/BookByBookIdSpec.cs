using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System;
using System.Linq;

namespace BookActivity.Domain.Filters.Specifications.BookSpecs
{
    public sealed class BookByBookIdSpec : IQueryableFilterSpec<Book, Guid>
    {
        public IQueryable<Book> ApplyFilter(IQueryable<Book> query, Guid bookId) 
        {
            return query.Where(b => b.Id == bookId);
        }
    }
}
