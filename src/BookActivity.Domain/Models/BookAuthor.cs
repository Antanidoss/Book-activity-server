using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public class BookAuthor : BaseEntity
    {
        public Book Book { get; set; }
        public Guid BookId { get; set; }
        public Author Author { get; set; }
        public  Guid AuthorId { get; set; }
    }
}
