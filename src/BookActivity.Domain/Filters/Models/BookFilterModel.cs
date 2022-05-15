using BookActivity.Domain.Models;
using System;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class BookFilterModel : BaseFilterModel
    {
        public FilterModelProp<Book, Guid> BookId { get; set; }

        public FilterModelProp<Book, string> Title { get; set; }

        public BookFilterModel(int skip = _skip, int take = _take) : base(skip, take) { }

        public BookFilterModel(
            FilterModelProp<Book, Guid> bookId,
            FilterModelProp<Book, string> title,
            int skip = _skip,
            int take = _take) : base(skip, take)
        {
            BookId = bookId;
            Title = title;
        }
    }
}
