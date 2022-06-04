using BookActivity.Domain.Models;
using System;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class ActiveBookFilterModel : BaseFilterModel
    {
        public readonly FilterModelProp<ActiveBook, Guid[]> ActiveBookIds;

        public readonly FilterModelProp<ActiveBook, Guid> UserId;

        public ActiveBookFilterModel(
            FilterModelProp<ActiveBook, Guid[]> activeBookIds,
            FilterModelProp<ActiveBook, Guid> userId,
            int skip = SkipDefault,
            int take = TakeDefault) : base(skip, take)
        {
            ActiveBookIds = activeBookIds;
            UserId = userId;
        }
        public ActiveBookFilterModel(int skip = SkipDefault, int take = TakeDefault) : base(skip, take) { }
    }
}
