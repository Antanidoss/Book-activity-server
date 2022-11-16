namespace BookActivity.Domain.Queries.ActiveBookStatisticQueries
{
    public sealed class ActiveBookStatistics
    {
        public float AveragePagesReadPerDay { get; set; }
        public float AveragePagesReadPerWeek { get; set; }
        public float AveragePagesReadPerMouth { get; set; }
    }
}
