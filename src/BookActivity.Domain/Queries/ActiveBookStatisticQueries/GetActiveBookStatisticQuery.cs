using System;

namespace BookActivity.Domain.Queries.ActiveBookStatisticQueries
{
    public sealed class GetActiveBookStatisticQuery : Query<ActiveBookStatistics>
    {
        public Guid AppUserId { get; set; }
    }
}
