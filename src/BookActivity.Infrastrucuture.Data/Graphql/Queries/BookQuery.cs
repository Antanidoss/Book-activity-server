using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.BookSpecs;
using BookActivity.Infrastructure.Data.Context;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate;
using System.Linq;
using System;

namespace BookActivity.Infrastructure.Data.Graphql.Queries
{
    [ExtendObjectType("Query")]
    public class BookQuery
    {
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Book> GetBooks([Service] BookActivityContext context, int averageRatingFrom = BookOpinion.GradeMin, int averageRatingTo = BookOpinion.GradeMax)
        {
            if (averageRatingFrom != BookOpinion.GradeMin || averageRatingTo != BookOpinion.GradeMax)
            {
                BookByRatingRange bookByRatingRangeSpec = new(averageRatingFrom, averageRatingTo);

                return context.Books.Where(bookByRatingRangeSpec);
            }

            return context.Books;
        }

        public Book GetBookById([Service] BookActivityContext context, Guid bookId)
        {
            BookByIdSpec specification = new(bookId);

            return context.Books.FirstOrDefault(specification);
        }
    }
}
