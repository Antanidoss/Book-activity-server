using BookActivity.Domain.Core;
using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Models
{
    public sealed class Author : BaseEntity
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
        /// Relation of author with the book authors
        /// </summary>
        public ICollection<BookAuthor> BookAuthors { get; set; }

        private Author() : base() { }
        public Author(Guid authorId) : base()
        {
            Id = authorId;
        }
        public Author(string firstName, string surname)
        {
            FirstName = firstName;
            Surname = surname;
            BookAuthors = new List<BookAuthor>();
        }
    }
}