using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Queries.ActiveBookStatisticQueries
{
    public sealed class ActiveBooksStatistic
    {
        public IEnumerable<(int CountPagesRead, DateTime Date)> NumberOfPagesReadPerDay { get; set; }
        public float AveragePagesReadPerDay { get; set; }
        public float AveragePagesReadPerWeek { get; set; }
        public float AveragePagesReadPerMouth { get; set; }
        public int AmountDaysOfReads { get; set; }
    }
}
