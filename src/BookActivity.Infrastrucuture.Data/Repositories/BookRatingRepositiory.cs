using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.EF;
using BookActivity.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
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

        public async Task<BookRating> GetByFilterAsync(DbSingleResultFilterModel<BookRating> filterModel)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = _dbSet.IncludeMultiple(filterModel.Includes);

            if (!filterModel.ForUpdate)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(filterModel.Specification);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
