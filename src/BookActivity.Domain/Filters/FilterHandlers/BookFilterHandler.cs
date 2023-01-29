using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Domain.Queries.BookQueries.GetBookByFilterQuery;
using BookActivity.Domain.Specifications.BookSpecs;
using System.Linq;

namespace BookActivity.Domain.Filters.Handlers
{
    internal sealed class BookFilterHandler : IFilterHandler<Book, GetBooksByFilterQuery>
    {
        public IQueryable<Book> ApplyFilter(IQueryable<Book> query, GetBooksByFilterQuery filterModel)
        {
            if (!string.IsNullOrEmpty(filterModel.BookTitle))
            {
                BookByTitleContainsSpec bookByTitleSpec = new(filterModel.BookTitle);

                query = query.Where(bookByTitleSpec);
            }

            if (filterModel.AverageRatingFrom != BookOpinion.GradeMin || filterModel.AverageRatingTo != BookOpinion.GradeMax)
            {
                BookByRatingRange bookByRatingRangeSpec = new(filterModel.AverageRatingFrom, filterModel.AverageRatingTo);

                query = query.Where(bookByRatingRangeSpec);
            }

            return query;
        }
    }
}
