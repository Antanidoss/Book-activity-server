using System;

namespace BookActivity.Application.Models.DTO.Filters
{
    public class ActiveBookDTOFilterModel : BaseDTOFilterModel
    {
        public Guid[] ActiveBookIds { get; set; }

        public Guid UserId { get; set; }
    }
}
