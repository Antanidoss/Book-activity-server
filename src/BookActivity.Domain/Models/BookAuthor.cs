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

        public BookAuthor(Author author)
        {
            Author = author;
        }

        public BookAuthor(Guid bookId, Guid authorId)
        {
            BookId = bookId;
            AuthorId = authorId;
        }

        public BookAuthor(Guid authorId)
        {
            AuthorId = authorId;
        }

        //private, parameterless constructor used by EF Core
        private BookAuthor() { }
    }
}
