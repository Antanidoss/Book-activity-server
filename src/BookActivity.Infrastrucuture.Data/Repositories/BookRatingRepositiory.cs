using BookActivity.Domain.Filters;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetDevPack.Data;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class BookRatingRepositiory : BaseRepository, IBookRatingRepository
    {
        private readonly DbSet<BookRating> _dbSet;

        public IUnitOfWork UnitOfWork => Context;

        public BookRatingRepositiory(BookActivityContext context, ILogger logger) : base(context, logger)
        {
            _dbSet = Context.Set<BookRating>();
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
            Context.Dispose();
        }
    }
}
