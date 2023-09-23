using System;

namespace BookActivity.Application.Models.Dto.Create
{
    public sealed class CreateActiveBookDto : BaseCreateDto
    {
        public int TotalNumberPages { get; set; }
        public int NumberPagesRead { get; set; }
        public Guid BookId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Guid UserId { get; set; }
        public bool IsPublic { get; set; }
    }
}
