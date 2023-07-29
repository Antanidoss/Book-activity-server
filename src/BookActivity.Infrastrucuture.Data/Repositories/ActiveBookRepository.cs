using BookActivity.Domain.Filters;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class ActiveBookRepository : BaseRepository, IActiveBookRepository
    {
        private readonly DbSet<ActiveBook> _dbSet;

        public IUnitOfWork UnitOfWork => Context;

        public ActiveBookRepository(BookActivityContext context, ILogger logger) : base(context, logger)
        {
            _dbSet = Context.Set<ActiveBook>();
        }

        public async Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<ActiveBook, TResult> filterModel)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = _dbSet.IncludeMultiple(filterModel.Includes);

            if (!filterModel.ForUpdate)
                query = query.AsNoTracking();

            if (typeof(TResult) == typeof(IEnumerable<ActiveBook>))
                query = query.ApplyPaginaton(filterModel.PaginationModel);

            return await filterModel.Filter(query);
        }

        public async Task<ActiveBook> GetByFilterAsync(DbSingleResultFilterModel<ActiveBook> filterModel)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = _dbSet.IncludeMultiple(filterModel.Includes);

            if (!filterModel.ForUpdate)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(filterModel.Specification);
        }

        public async Task<int> GetCountByFilterAsync(DbMultipleResultFilterModel<ActiveBook> filterModel)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = await filterModel.Filter(_dbSet);

            return await query
                .ApplyPaginaton(filterModel.PaginationModel)
                .CountAsync();
        }

        public void Add(ActiveBook activeBook)
        {
            CommonValidator.ThrowExceptionIfNull(activeBook);

            _dbSet.Add(activeBook);
        }

        public void Remove(ActiveBook activeBook)
        {
            CommonValidator.ThrowExceptionIfNull(activeBook);

            _dbSet.Remove(activeBook);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}