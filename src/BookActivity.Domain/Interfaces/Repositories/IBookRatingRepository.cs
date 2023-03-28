using BookActivity.Domain.Filters;
using BookActivity.Domain.Models;
using NetDevPack.Data;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IBookRatingRepository : IRepository<BookRating>
    {
        Task<BookRating> GetByFilterAsync(DbSingleResultFilterModel<BookRating> filterModel);
    }
}
