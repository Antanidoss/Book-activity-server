using BookActivity.Domain.Queries.ActiveBookQueries.GetActiveBookByFilter;
using System;

namespace BookActivity.Application.Models.Dto.Read
{
    public class GetActiveBookByFilterDto
    {
        public string BookTitle { get; set; }
        public bool WithFullRead { get; set; } = true;
        public SortByType SortBy { get; set; } = SortByType.CreateDate;
        public int Skip { get; set; }
        public int Take { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Guid UserId { get; set; }
    }
}
