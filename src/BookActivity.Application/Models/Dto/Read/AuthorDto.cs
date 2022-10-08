using System;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class AuthorDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
    }
}
