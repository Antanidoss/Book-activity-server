using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using HotChocolate.Data;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;

namespace BookActivity.Infrastructure.Data.Graphql.Queries
{
    [ExtendObjectType("Query")]
    public class BookRatingQuery
    {
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<BookRating> GetBookRatings([Service] BookActivityContext context)
        {
            return context.BookRatings;
        }
    }
}
