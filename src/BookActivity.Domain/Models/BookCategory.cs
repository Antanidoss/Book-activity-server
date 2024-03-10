using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public sealed class BookCategory : BaseEntity
    {
        public Book Book { get; set; }
        public Guid BookId { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}