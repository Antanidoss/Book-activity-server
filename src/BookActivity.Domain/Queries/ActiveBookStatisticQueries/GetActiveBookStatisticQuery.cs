using System;

namespace BookActivity.Domain.Queries.ActiveBookStatisticQueries
{
    public sealed class GetActiveBookStatisticQuery : Query<ActiveBooksStatistic>
    {
        public Guid AppUserId { get; set; }

        public GetActiveBookStatisticQuery(Guid appUserId)
        {
            AppUserId = appUserId;
        }
    }
}
