using BookActivity.Domain.Models;
using System;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class BookFilterModel : BaseFilterModel
    {
        public FilterModelProp<Book, Guid[]> BookIds { get; set; }

        public FilterModelProp<Book, string> Title { get; set; }

        public BookFilterModel(int skip = SkipDefault, int take = TakeDefault) : base(skip, take) { }

        public BookFilterModel(
            FilterModelProp<Book, Guid[]> bookIds,
            FilterModelProp<Book, string> title,
            int skip = SkipDefault,
            int take = TakeDefault) : base(skip, take)
        {
            BookIds = bookIds;
            Title = title;
        }
    }
}
