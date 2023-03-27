using BookActivity.Domain.Filters;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class AuthorRepository : IAuthorRepository
    {
        private readonly BookActivityContext _db;

        private readonly DbSet<Author> _dbSet;

        public IUnitOfWork UnitOfWork => _db;

        public AuthorRepository(BookActivityContext context)
        {
            _db = context;
            _dbSet = _db.Set<Author>();
        }

        public async Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<Author, TResult> filterModel)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = _dbSet.IncludeMultiple(filterModel.Includes);

            if (!filterModel.ForUpdate)
                query = query.AsNoTracking();

            if (typeof(TResult) == typeof(IEnumerable<Author>))
                query = query.ApplyPaginaton(filterModel.PaginationModel);

            return await filterModel.Filter(query);
        }

        public async Task<Author> GetByFilterAsync(DbSingleResultFilterModel<Author> filterModel)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = _dbSet.IncludeMultiple(filterModel.Includes);

            if (!filterModel.ForUpdate)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(filterModel.Specification);
        }

        public async Task<int> GetCountByFilterAsync(DbMultipleResultFilterModel<Author> filterModel)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = await filterModel.Filter(_dbSet);

            return await query
                .ApplyPaginaton(filterModel.PaginationModel)
                .CountAsync();
        }

        public void Add(Author entity)
        {
            CommonValidator.ThrowExceptionIfNull(entity);

            _dbSet.Add(entity);
        }

        public void Remove(Author entity)
        {
            CommonValidator.ThrowExceptionIfNull(entity);

            _dbSet.Remove(entity);
        }

        public void Update(Author entity)
        {
            CommonValidator.ThrowExceptionIfNull(entity);

            _dbSet.Update(entity);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
