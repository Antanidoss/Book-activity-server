using BookActivity.Domain.Models;
using System;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class ActiveBookFilterModel : BaseFilterModel
    {
        public readonly FilterModelProp<ActiveBook, Guid> ActiveBookId;

        public readonly FilterModelProp<ActiveBook, Guid> UserId;

        public ActiveBookFilterModel(
            FilterModelProp<ActiveBook, Guid> activeBook,
            FilterModelProp<ActiveBook, Guid> userId,
            int skip = _skip,
            int take = _take) : base(skip, take)
        {
            ActiveBookId = activeBook;
            UserId = userId;
        }
        public ActiveBookFilterModel(int skip = _skip, int take = _take) : base(skip, take) { }
    }
}
