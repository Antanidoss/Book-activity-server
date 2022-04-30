using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using BookActivity.Models.Filters.Specifications.Book;
using System;
using System.Linq;

namespace BookActivity.Domain.Filters.FilterFacades
{
    public sealed class BookFilter : BaseFilter<Book>
    {
        private readonly BookFilterModel _bookFilterModel;
        public BookFilter(BookFilterModel bookFilterModel) : base(bookFilterModel)
        {
            _bookFilterModel = bookFilterModel;
        }

        public override IQueryable<Book> ApplyFilter(IQueryable<Book> query)
        {
            if (query == null) return null;

            if (_bookFilterModel.BookId != Guid.Empty)
            {
                BookByBookIdSpec bookByBookIdSpec = new(_bookFilterModel.BookId);
                query = bookByBookIdSpec.Apply(query);
            }
            else if (!string.IsNullOrEmpty(_bookFilterModel.Title))
            {
                BookByTitleSpec bookByTitleSpec = new(_bookFilterModel.Title);
                query = bookByTitleSpec.Apply(query);
            }

            return base.ApplyFilter(query);
        }
    }
}
