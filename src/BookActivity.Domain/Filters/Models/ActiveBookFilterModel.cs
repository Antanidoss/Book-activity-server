using System;

namespace BookActivity.Domain.Filters.Models
{
    public class ActiveBookFilterModel : BaseFilterModel
    {
        public readonly Guid ActiveBookId;

        public readonly Guid UserId;

        public ActiveBookFilterModel(Guid activeBookId, Guid userId, int skip, int take) : base(skip, take)
        {
            ActiveBookId = activeBookId;
            UserId = userId;
        }
    }
}
