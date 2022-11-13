using BookActivity.Domain.Models;
using BookActivity.Domain.Queries.BookQueries;
using BookActivity.Domain.Specifications.BookSpecs;
using System.Linq;

namespace BookActivity.Domain.Filters.Handlers
{
    public static class BookFilterHandler
    {
        public static IQueryable<Book> ApplyBookFilter(this IQueryable<Book> query, GetBooksByFilterQuery filterModel)
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

            return query.Select(b => new Book
            {
                Id = b.Id,
                Title = b.Title,
                ImageData = b.ImageData,
                BookRating = new BookRating
                {
                    Id = b.BookRating.Id,
                    BookOpinions = b.BookRating.BookOpinions.Select(r => new BookOpinion
                    {
                        Id = r.Id,
                        Grade = r.Grade,
                    }).ToList()
                }
            });
        }
    }
}
