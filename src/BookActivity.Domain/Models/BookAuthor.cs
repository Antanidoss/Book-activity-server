using System.Collections.Generic;

namespace BookActivity.Domain.Models
{
    public class BookAuthor : BaseEntity
    {
        /// <summary>
        /// Firstname author
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Surname author
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Patronymic author
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Relation of author with the book authors
        /// </summary>
        public ICollection<Book> Books { get; set; }

        protected BookAuthor() : base() { }
        public BookAuthor(string firstName, string surname, string patronymic, bool isPublic, params Book[] books) : base(isPublic)
        {
            FirstName = firstName;
            Surname = surname;
            Patronymic = patronymic;
            Books = books;
        }
    }
}