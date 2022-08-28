using System;

namespace BookActivity.Application.Models.DTO.Create
{
    public sealed class CreateActiveBookDTO : BaseCreateDTO
    {
        public int TotalNumberPages { get; set; }
        public int NumberPagesRead { get; set; }
        public Guid BookId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Guid UserId { get; set; } 
        public bool IsPublic { get; set; }
        public CreateActiveBookDTO() { }
    }
}