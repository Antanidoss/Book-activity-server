using BookActivity.Domain.Models;
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
        public IEnumerable<SelectedBookNote> BookNotes { get; set; }
    }
}
