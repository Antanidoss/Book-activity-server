using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System;
using System.Linq;

namespace BookActivity.Domain.Filters.Specifications.BookSpecs
{
    public sealed class BookByBookIdSpec : IQueryableFilterSpec<Book, Guid[]>
    {
        public IQueryable<Book> ApplyFilter(IQueryable<Book> books, Guid[] bookIds) 
        {
            return books.Where(b => bookIds.Contains(b.Id));
        }
    }
}
