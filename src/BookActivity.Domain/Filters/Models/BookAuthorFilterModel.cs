using BookActivity.Domain.Models;
using System;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class BookAuthorFilterModel : BaseFilterModel
    {
        public readonly FilterModelProp<BookAuthor, Guid[]> AuthorIds;

        public BookAuthorFilterModel(int skip = _skip, int take = _take) : base(skip, take) { }
        public BookAuthorFilterModel(FilterModelProp<BookAuthor, Guid[]> authorIds, int skip = _skip, int take = _take) : base(skip, take)
        {
            AuthorIds = authorIds;
        }
    }
}
