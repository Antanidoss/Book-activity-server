using Antanidoss.Specification.Filters.Interfaces;
using BookActivity.Domain.Models;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class ActiveBookFilterModel : BaseFilterModel
    {
        public IQueryableMultipleResultFilter<ActiveBook> Filter { get; set; }

        public ActiveBookFilterModel(int? skip = SkipDefault, int? take = TakeDefault) : base(skip, take) { }
        public ActiveBookFilterModel(IQueryableMultipleResultFilter<ActiveBook> filter, int? skip = SkipDefault, int? take = TakeDefault) : base(skip, take)
        {
            Filter = filter;
        }
    }
}
