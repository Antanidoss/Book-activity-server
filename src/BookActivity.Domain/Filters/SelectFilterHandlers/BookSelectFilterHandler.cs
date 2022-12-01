using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Domain.Queries.BookQueries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Domain.Filters.SelectFilterHandlers
{
    internal class BookSelectFilterHandler : IFilterSelectHandler<Book, IEnumerable<SelectedBook>, GetBooksByFilterQuery>
    {
        public async Task<IEnumerable<SelectedBook>> Select(IQueryable<Book> query, GetBooksByFilterQuery filterModel)
        {
            return query.Select(b => new SelectedBook
            {
                Id = b.Id,
                Title = b.Title,
                ImageData = b.ImageData,
                IsActiveBook =  b.ActiveBooks.Any(a => a.UserId == filterModel.UserId),
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
