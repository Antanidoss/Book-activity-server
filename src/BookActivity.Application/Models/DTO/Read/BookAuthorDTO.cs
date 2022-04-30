using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Read
{
    public sealed class BookAuthorDTO : BaseEntityDTO
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public ICollection<BookDTO> Books { get; set; }
        
        public BookAuthorDTO() : base() { }
        public BookAuthorDTO(
            Guid bookAuthorId,
            DateTime timeOfCreation,
            DateTime timeOfUpdate,
            bool isPublic,
            string firstName,
            string surname,
            string patronymic,
            ICollection<BookDTO> books) : base(bookAuthorId, timeOfCreation, timeOfUpdate, isPublic)
        {
            FirstName = firstName;
            Surname = surname;
            Patronymic = patronymic;
            Books = books;
        }
    }
}