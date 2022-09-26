using BookActivity.Application.Models.DTO.Read;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class AuthorDto : BaseEntityDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
    }
}
