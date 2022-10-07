using Antanidoss.Specification.Filters.Interfaces;
using BookActivity.Domain.Models;
using NetDevPack.Data;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IBookRatingRepository : IRepository<BookRating>
    {
        BookRating GetByFilterAsync(IQueryableSingleResultFilter<BookRating> filter);
    }
}
