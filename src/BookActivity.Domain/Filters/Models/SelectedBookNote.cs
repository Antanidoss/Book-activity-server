using System;

namespace BookActivity.Domain.Filters.Models
{
    public class SelectedBookNote
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        public int NoteColor { get; set; }
    }
}
