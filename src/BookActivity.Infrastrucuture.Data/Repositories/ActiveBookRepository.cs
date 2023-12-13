using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.EF;
using BookActivity.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class ActiveBookRepository : IActiveBookRepository
    {
        private readonly BookActivityContext _db;

        private readonly DbSet<ActiveBook> _dbSet;

        public IUnitOfWork UnitOfWork => _db;

        public ActiveBookRepository(BookActivityContext context)
        {
            _db = context;
            _dbSet = _db.Set<ActiveBook>();
        }

        public async Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<ActiveBook, TResult> filterModel, CancellationToken cancellationToken = default)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = _dbSet.IncludeMultiple(filterModel.Includes);

            if (!filterModel.ForUpdate)
                query = query.AsNoTracking();

            if (typeof(TResult) == typeof(IEnumerable<ActiveBook>))
                query = query.ApplyPaginaton(filterModel.PaginationModel);

            return await filterModel.Filter(query);
        }

        public async Task<ActiveBook> GetByFilterAsync(DbSingleResultFilterModel<ActiveBook> filterModel, CancellationToken cancellationToken = default)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = _dbSet.IncludeMultiple(filterModel.Includes);

            if (!filterModel.ForUpdate)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(filterModel.Specification);
        }

        public async Task<int> GetCountByFilterAsync(DbMultipleResultFilterModel<ActiveBook> filterModel, CancellationToken cancellationToken = default)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = await filterModel.Filter(_dbSet);

            return await query
                .ApplyPaginaton(filterModel.PaginationModel)
                .CountAsync(cancellationToken);
        }

        public async Task AddAsync(ActiveBook activeBook, CancellationToken cancellationToken = default)
        {
            CommonValidator.ThrowExceptionIfNull(activeBook);

            await _dbSet.AddAsync(activeBook, cancellationToken);
        }

        public void Remove(ActiveBook activeBook)
        {
            CommonValidator.ThrowExceptionIfNull(activeBook);

            _dbSet.Remove(activeBook);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}