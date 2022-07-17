using System;

namespace BookActivity.Application.Models.HistoryData
{
    public sealed class BookHistoryData : BaseHistoryData
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
