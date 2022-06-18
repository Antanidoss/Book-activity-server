using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class ActiveBookFilterModel : BaseFilterModel
    {
        public IQueryableFilterSpec<ActiveBook> Filter { get; set; }

        public ActiveBookFilterModel(IQueryableFilterSpec<ActiveBook> filter, int? skip = SkipDefault,int? take = TakeDefault) : base(skip, take)
        {
            Filter = filter;
        }

        public ActiveBookFilterModel(int skip = SkipDefault, int take = TakeDefault) : base(skip, take) { }
    }
}
