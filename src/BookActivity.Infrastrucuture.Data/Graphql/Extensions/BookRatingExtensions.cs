using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate;
using System.Linq;

namespace BookActivity.Infrastructure.Data.Graphql.Extensions
{
    [ExtendObjectType(typeof(BookRating))]
    public class BookRatingExtensions
    {
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<BookOpinion> GetBookOpinions([Service] BookActivityContext context, [Parent] BookRating bookRating)
        {
            return context.BookOpinions.Where(b => b.BookRatingId == bookRating.Id);
        }
    }
}
