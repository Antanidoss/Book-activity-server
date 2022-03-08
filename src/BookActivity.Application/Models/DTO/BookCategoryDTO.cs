using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO
{
    public class BookCategoryDTO : BaseEntityDTO
    {
        public string Title { get; set; }
        public ICollection<BookDTO> Books { get; set; }
    }
}