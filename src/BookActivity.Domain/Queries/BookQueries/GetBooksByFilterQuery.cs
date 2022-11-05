using BookActivity.Domain.Models;
using System.Collections.Generic;

namespace BookActivity.Domain.Queries.BookQueries
{
    public sealed class GetBooksByFilterQuery : Query<IEnumerable<Book>>
    {
        public string BookTitle { get; set; }
        public float AverageRatingFrom { get; set; }
        public float AverageRatingTo { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
