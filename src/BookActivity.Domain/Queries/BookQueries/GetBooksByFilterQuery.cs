using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using BookActivity.Shared.Models;
using System;

namespace BookActivity.Domain.Queries.BookQueries
{
    public sealed class GetBooksByFilterQuery : Query<EntityListResult<SelectedBook>>
    {
        public string BookTitle { get; set; }
        public float AverageRatingFrom { get; set; }
        public float AverageRatingTo { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public Guid UserId { get; set; }
    }
}
