using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.Validations;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class BookRatingRepositiory : IBookRatingRepository
    {
        private readonly BookActivityContext _db;

        private readonly DbSet<BookRating> _dbSet;

        public IUnitOfWork UnitOfWork => _db;

        public BookRatingRepositiory(BookActivityContext context)
        {
            _db = context;
            _dbSet = _db.Set<BookRating>();
        }

        public async Task<BookRating> GetBySpecAsync(ISpecification<BookRating> specification)
        {
            SpecificationValidator.ThrowExceptionIfNull(specification);

            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(specification.ToExpression());
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
