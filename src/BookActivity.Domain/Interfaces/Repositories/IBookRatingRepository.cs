using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Models;
using NetDevPack.Data;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IBookRatingRepository : IRepository<BookRating>
    {
        Task<BookRating> GetBySpecAsync(ISpecification<BookRating> specification);
    }
}
