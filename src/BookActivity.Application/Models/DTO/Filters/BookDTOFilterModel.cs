using System;

namespace BookActivity.Application.Models.DTO.Filters
{
    public class BookDTOFilterModel : BaseDTOFilterModel
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }
    }
}
