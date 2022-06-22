using BookActivity.Domain.Models;
using QueryableFilterSpecification.Interfaces;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class AppUserFilterModel : BaseFilterModel
    {
        public IQueryableFilterSpec<AppUser> Filter { get; set; }

        public AppUserFilterModel(int skip = SkipDefault, int take = TakeDefault) : base(skip, take) { }

        public AppUserFilterModel(IQueryableFilterSpec<AppUser> filter, int skip = SkipDefault, int take = TakeDefault) : base(skip, take)
        {
            Filter = filter;
        }
    }
}
