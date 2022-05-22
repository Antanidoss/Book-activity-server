using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System;
using System.Linq;

namespace BookActivity.Domain.Filters.Specifications.BookSpecs
{
    public sealed class BookByBookIdSpec : IQueryableFilterSpec<Book, Guid>
    {
        public IQueryable<Book> ApplyFilter(IQueryable<Book> books, Guid bookId) 
        {
            return books.Where(b => b.Id == bookId);
        }
    }
}
