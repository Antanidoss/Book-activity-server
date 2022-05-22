using BookActivity.Domain.Models;
using System;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class AppUserFilterModel : BaseFilterModel
    {
        public FilterModelProp<AppUser, Guid> AppUserId { get; set; }
        public FilterModelProp<AppUser, string> Email { get; set; }

        public AppUserFilterModel(int skip = _skip, int take = _take) : base(skip, take) { }
    }
}
