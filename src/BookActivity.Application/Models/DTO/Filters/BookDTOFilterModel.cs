using System;

namespace BookActivity.Application.Models.DTO.Filters
{
    public class BookDTOFilterModel : BaseDTOFilterModel
    {
        public Guid[] BookIds { get; set; }

        public string Title { get; set; }
    }
}
