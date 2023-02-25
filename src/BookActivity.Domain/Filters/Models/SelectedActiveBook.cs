using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Filters.Models
{
    public class SelectedActiveBook
    {
        public Guid Id { get; set; }
        public int TotalNumberPages { get; set; }
        public int NumberPagesRead { get; set; }
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }
        public byte[] ImageData { get; set; }
        public IEnumerable<SelectedBookNote> BookNotes { get; set; }
        public Guid BookRatingId { get; set; }
        public SelectedBookOpinion BookOpinion { get; set; }

        public class SelectedBookOpinion
        {
            public Guid Id { get; set; }
            public float Grade { get; set; }
            public string Description { get; set; }
        }
    }
}
