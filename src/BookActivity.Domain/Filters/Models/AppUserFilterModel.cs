using BookActivity.Domain.Models;
using System;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class AppUserFilterModel : BaseFilterModel
    {
        public FilterModelProp<AppUser, Guid> AppUserId { get; set; }
        public FilterModelProp<AppUser, string> Email { get; set; }

        public AppUserFilterModel(int skip = SkipDefault, int take = TakeDefault) : base(skip, take) { }
    }
}
