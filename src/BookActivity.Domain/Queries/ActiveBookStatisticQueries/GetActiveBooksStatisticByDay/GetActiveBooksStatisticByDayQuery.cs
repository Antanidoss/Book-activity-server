using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatisticByDay
{
    public sealed class GetActiveBooksStatisticByDayQuery : Query<IEnumerable<ActiveBookStatisticByDay>>
    {
        public DateTime Day { get; set; }

        public Guid AppUserId {  get; set; }

        public GetActiveBooksStatisticByDayQuery(DateTime day, Guid appUserId)
        {
            Day = day;
            AppUserId = appUserId;
        }
    }
}
