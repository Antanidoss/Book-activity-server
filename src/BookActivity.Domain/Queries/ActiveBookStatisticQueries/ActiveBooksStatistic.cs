namespace BookActivity.Domain.Queries.ActiveBookStatisticQueries
{
    public sealed class ActiveBooksStatistic
    {
        public float AveragePagesReadPerDay { get; set; }
        public float AveragePagesReadPerWeek { get; set; }
        public float AveragePagesReadPerMouth { get; set; }
        public int AmountDaysOfReads { get; set; }
    }
}
