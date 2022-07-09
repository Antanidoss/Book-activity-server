using Antanidoss.Specification.Filters.Interfaces;
using BookActivity.Domain.Models;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class BookFilterModel : BaseFilterModel
    {
        public IQueryableMultipleResultFilter<Book> Filter { get; set; }

        public BookFilterModel(int? skip = SkipDefault, int? take = TakeDefault) : base(skip, take) { }

        public BookFilterModel(IQueryableMultipleResultFilter<Book> filter, int? skip = SkipDefault, int? take = TakeDefault) : base(skip, take)
        {
            Filter = filter;
        }
    }
}
