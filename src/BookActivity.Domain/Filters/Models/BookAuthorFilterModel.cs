using Antanidoss.Specification.Filters.Interfaces;
using BookActivity.Domain.Models;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class BookAuthorFilterModel : BaseFilterModel
    {
        public IQueryableMultipleResultFilter<BookAuthor> Filter { get; set; }

        public BookAuthorFilterModel(int skip = SkipDefault, int take = TakeDefault) : base(skip, take) { }
        public BookAuthorFilterModel(IQueryableMultipleResultFilter<BookAuthor> filter, int skip = SkipDefault, int take = TakeDefault) : base(skip, take)
        {
            Filter = filter;
        }
    }
}
