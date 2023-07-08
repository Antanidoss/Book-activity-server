using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Queries.ActiveBookStatisticQueries
{
    public sealed class ActiveBooksStatistic
    {
        public IEnumerable<NumberOfPagesReadPerDay> ReadingCalendar { get; set; }
        public float AveragePagesReadPerDay { get; set; }
        public float AveragePagesReadPerWeek { get; set; }
        public float AveragePagesReadPerMouth { get; set; }
        public int AmountDaysOfReads { get; set; }
    }

    public class NumberOfPagesReadPerDay
    {
        public int CountPagesRead { get; set; }
        public string Date { get; set; }
    }
}
