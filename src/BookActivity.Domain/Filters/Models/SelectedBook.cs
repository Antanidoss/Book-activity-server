using BookActivity.Domain.Models;
using System;

namespace BookActivity.Domain.Filters.Models
{
    public class SelectedBook
    {
        public Guid Id { get; set; }
        public bool IsActiveBook { get; set; }
        public string Title { get; set; }
        public byte[] ImageData { get; set; }
        public BookRating BookRating { get; set; }
    }
}
