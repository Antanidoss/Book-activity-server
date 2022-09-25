using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Read
{
    public sealed class BookAuthorDto : BaseEntityDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public ICollection<BookDto> Books { get; set; }
        
        public BookAuthorDto() : base() { }
        public BookAuthorDto(
            Guid bookAuthorId,
            DateTime timeOfCreation,
            DateTime timeOfUpdate,
            bool isPublic,
            string firstName,
            string surname,
            string patronymic,
            ICollection<BookDto> books) : base(bookAuthorId, timeOfCreation, timeOfUpdate, isPublic)
        {
            FirstName = firstName;
            Surname = surname;
            Patronymic = patronymic;
            Books = books;
        }
    }
}