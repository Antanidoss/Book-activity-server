using System;

namespace BookActivity.Application.EventSourcedNormalizers.Models
{
    public sealed class ActiveBookHistoryData : BaseHistoryData
    {
        public Guid ActiveBookId { get; set; }
        public int TotalNumberPages { get; set; }
        public int NumberPagesRead { get; set; }
    }
}
