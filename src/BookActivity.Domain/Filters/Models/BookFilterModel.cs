using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class BookFilterModel : BaseFilterModel
    {
        public IQueryableFilterSpec<Book> Filter { get; set; }

        public BookFilterModel(int skip = SkipDefault, int take = TakeDefault) : base(skip, take) { }

        public BookFilterModel(IQueryableFilterSpec<Book> filter, int skip = SkipDefault, int take = TakeDefault) : base(skip, take)
        {
            Filter = filter;
        }
    }
}
