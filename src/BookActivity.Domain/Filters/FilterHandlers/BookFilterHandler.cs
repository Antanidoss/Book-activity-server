using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Domain.Queries.BookQueries;
using BookActivity.Domain.Specifications.BookSpecs;
using System.Linq;

namespace BookActivity.Domain.Filters.Handlers
{
    internal class BookFilterHandler : IFilterHandler<Book, GetBooksByFilterQuery>
    {
        public IQueryable<Book> ApplyFilter(IQueryable<Book> query, GetBooksByFilterQuery filterModel)
        {
            if (!string.IsNullOrEmpty(filterModel.BookTitle))
            {
                BookByTitleContainsSpec bookByTitleSpec = new(filterModel.BookTitle);

                query = query.Where(bookByTitleSpec.ToExpression());
            }

            if (filterModel.AverageRatingFrom != BookOpinion.GradeMin || filterModel.AverageRatingTo != BookOpinion.GradeMax)
            {
                BookByRatingRange bookByRatingRangeSpec = new(filterModel.AverageRatingFrom, filterModel.AverageRatingTo);

                query = query.Where(bookByRatingRangeSpec.ToExpression());
            }

            return query;
        }
    }
}
