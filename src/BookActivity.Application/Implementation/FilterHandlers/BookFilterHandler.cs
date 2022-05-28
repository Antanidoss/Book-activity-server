using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Application.Implementation.FilterHandlers
{
    internal sealed class BookFilterHandler : IFilterHandler<Book, BookFilterModel>
    {
        public IQueryable<Book> Handle(BookFilterModel bookFilterModel, IQueryable<Book> query)
        {
            if (query == null) return null;

            if (bookFilterModel.BookId != null)
                query = bookFilterModel.BookId.FilterSpec.ApplyFilter(query, bookFilterModel.BookId.Value);

            if (bookFilterModel.Title != null)
                query = bookFilterModel.Title.FilterSpec.ApplyFilter(query, bookFilterModel.Title.Value);

            return query.Skip(bookFilterModel.Skip).Take(bookFilterModel.Take);
        }
    }
}
