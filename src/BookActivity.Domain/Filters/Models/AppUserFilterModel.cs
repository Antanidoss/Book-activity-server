using Antanidoss.Specification.Filters.Interfaces;
using BookActivity.Domain.Models;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class AppUserFilterModel : BaseFilterModel
    {
        public IQueryableMultipleResultFilter<AppUser> Filter { get; set; }

        public AppUserFilterModel(int skip = SkipDefault, int take = TakeDefault) : base(skip, take) { }

        public AppUserFilterModel(IQueryableMultipleResultFilter<AppUser> filter, int skip = SkipDefault, int take = TakeDefault) : base(skip, take)
        {
            Filter = filter;
        }
    }
}
