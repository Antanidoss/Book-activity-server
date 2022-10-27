using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using BookActivity.Domain.Specifications.BookSpecs;
using System.Linq;

namespace BookActivity.Application.Implementation.Filters
{
    public static class BookFilterHandler
    {
        public static IQueryable<Book> ApplyBookFilter(this IQueryable<Book> query, BookFilterModel filterModel)
        {
            if (!string.IsNullOrEmpty(filterModel.BookTitle))
            {
                BookByTitleContainsSpec bookByTitleSpec = new(filterModel.BookTitle);

                query = query.Where(bookByTitleSpec.ToExpression());
            }

            //if (filterModel.AverageRatingFrom != 0 || filterModel.AverageRatingTo != 5)
            //{
            //    BookByRatingRange bookByRatingRangeSpec = new(filterModel.AverageRatingFrom, filterModel.AverageRatingTo);

            //    query = query.Where(bookByRatingRangeSpec.ToExpression());
            //}

            return query;
        }
    }
}
