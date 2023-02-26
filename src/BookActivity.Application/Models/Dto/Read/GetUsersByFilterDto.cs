using System;

namespace BookActivity.Application.Models.Dto.Read
{
    public class GetUsersByFilterDto
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string Name { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Guid? CurrentUserId { get; set; }
    }
}
