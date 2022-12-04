using BookActivity.Domain.Filters.Models;
using BookActivity.Shared.Models;
using System;

namespace BookActivity.Domain.Queries.ActiveBookQueries.GetActiveBookByFilter
{
    public class GetActiveBookByFilterQuery : Query<EntityListResult<SelectedActiveBook>>
    {
        public string BookTitle { get; set; }
        public bool WithFullRead { get; set; } = true;
        public SortByType SortBy { get; set; } = SortByType.CreateDate;
        public Guid UserId { get; set; } 
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    public enum SortByType
    {
        CreateDate,
        CreateDateDescending,
        UpdateDateDescending,
    }
}
