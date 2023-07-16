using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.BookSpecs;
using BookActivity.Infrastructure.Data.Context;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System;
using System.Linq;

namespace BookActivity.Infrastructure.Data.Graphql
{
    public class Query
    {
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ActiveBook> GetActiveBooks([Service] BookActivityContext context, bool withFullRead = true)
        {
            if (!withFullRead)
                return context.ActiveBooks.Where(a => a.TotalNumberPages != a.NumberPagesRead);

            return context.ActiveBooks;
        }

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
    }
}
