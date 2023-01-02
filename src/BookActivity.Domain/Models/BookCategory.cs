using System.Collections.Generic;

namespace BookActivity.Domain.Models
{
    public sealed class BookCategory : BaseEntity
    {
        /// <summary>
        /// Title category
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Relation of book category with the book
        /// </summary>
        public ICollection<Book> Books { get; set; }

        private BookCategory() : base() { }
        public BookCategory(string title) : base()
        {
            Title = title;
            Books = new List<Book>();
        }
        public BookCategory(string title, params Book[] books)
        {
            Title = title;
            Books = books;
        }
    }
}