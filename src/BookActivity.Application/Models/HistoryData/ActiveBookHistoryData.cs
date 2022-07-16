using System;

namespace BookActivity.Application.Models.HistoryData
{
    public sealed class ActiveBookHistoryData : BaseHistoryData
    {
        public Guid ActiveBookId { get; set; }
        public int TotalNumberPages { get; set; }
        public int NumberPagesRead { get; set; }
    }
}
