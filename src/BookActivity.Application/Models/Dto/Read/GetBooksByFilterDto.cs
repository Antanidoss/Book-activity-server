using System;

namespace BookActivity.Application.Models.Dto.Read
{
    public class GetBooksByFilterDto
    {
        public string BookTitle { get; set; }
        public float AverageRatingFrom { get; set; }
        public float AverageRatingTo { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Guid UserId { get; set; }
    }
}
