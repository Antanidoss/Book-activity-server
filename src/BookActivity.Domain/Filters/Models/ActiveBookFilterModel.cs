using System;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class ActiveBookFilterModel : BaseFilterModel
    {
        public Guid ActiveBookId { get; set; }

        public Guid UserId { get; set; }

        public ActiveBookFilterModel(Guid activeBookId, Guid userId, int skip, int take) : base(skip, take)
        {
            ActiveBookId = activeBookId;
            UserId = userId;
        }
    }
}
