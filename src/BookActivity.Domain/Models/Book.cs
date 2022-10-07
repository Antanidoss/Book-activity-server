using System;
using System.Collections.Generic;
using System.Linq;

namespace BookActivity.Domain.Models
{
    public sealed class Book : BaseEntity
    {
        /// <summary>
        /// Book title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Book description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Relation of book with the book authors
        /// </summary>
        public IEnumerable<BookAuthor> BookAuthors { get; private set; }

        /// <summary>
        /// Image data
        /// </summary>
        public byte[] ImageData { get; set; }

        /// <summary>
        /// Relation of book with the active book
        /// </summary>
        public ICollection<ActiveBook> ActiveBooks { get; private set; }

        /// <summary>
        /// Relation of book with the book rating
        /// </summary>
        public BookRating BookRating { get; set; }

        private Book() : base() { }
        public Book(string title, string description, bool isPublic, byte[] imageData, IEnumerable<BookAuthor> bookAuthors) : base(isPublic)
        {
            Title = title;
            Description = description;
            BookAuthors = bookAuthors.ToList();
            ImageData = imageData;
            BookRating = new();
        }
    }
}