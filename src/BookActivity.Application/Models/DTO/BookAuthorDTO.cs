using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO
{
    public class BookAuthorDTO : BaseEntityDTO
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public ICollection<BookDTO> Books { get; set; }
    }
}