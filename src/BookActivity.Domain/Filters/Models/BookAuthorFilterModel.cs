using BookActivity.Domain.Models;
using System;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class BookAuthorFilterModel : BaseFilterModel
    {
        public FilterModelProp<BookAuthor, Guid[]> AuthorIds { get; set; }

        public BookAuthorFilterModel(int skip = _skip, int take = _take) : base(skip, take) { }
    }
}
