using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Domain.Filters.Specifications.BookSpecs
{
    public sealed class BookByTitleSpec : IQueryableFilterSpec<Book, string>
    {
        public IQueryable<Book> ApplyFilter(IQueryable<Book> books, string title)
        {
            return books.Where(b => b.Title == title);
        }
    }
}
