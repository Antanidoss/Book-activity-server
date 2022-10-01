using System;

namespace BookActivity.Application.Models.Dto.Create
{
    public class CreateBookOpinionDto
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid UserId { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
    }
}
