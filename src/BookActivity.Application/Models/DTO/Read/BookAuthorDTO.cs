using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class BookAuthorDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public ICollection<BookDto> Books { get; set; }
    }
}