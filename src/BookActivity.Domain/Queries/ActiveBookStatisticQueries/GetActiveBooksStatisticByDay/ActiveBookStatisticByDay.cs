using System;

namespace BookActivity.Domain.Queries.ActiveBookStatisticQueries.GetActiveBooksStatisticByDay
{
    public sealed class ActiveBookStatisticByDay
    {
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookImageData { get; set; }
        public int CountPagesRead { get; set; }
    }
}
