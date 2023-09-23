using System;

namespace BookActivity.Application.Models.Dto.Update
{
    public sealed class UpdateNumberPagesReadDto
    {
        public Guid ActiveBookId { get; set; }
        public int NumberPagesRead { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid UserId { get; set; }
    }
}
