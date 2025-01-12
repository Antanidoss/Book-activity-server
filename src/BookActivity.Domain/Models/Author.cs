using BookActivity.Domain.Core;
using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Models
{
    public sealed class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }

        public Author(Guid authorId) : base(authorId) { }

        public Author(string firstName, string surname) : base()
        {
            FirstName = firstName;
            Surname = surname;
            BookAuthors = new List<BookAuthor>();
        }

        //private, parameterless constructor used by EF Core
        private Author() { }
    }
}