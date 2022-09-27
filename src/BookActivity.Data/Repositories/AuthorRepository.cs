using Antanidoss.Specification.Filters.Interfaces;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.Helpers;
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

        public async Task<IEnumerable<Author>> GetByFilterAsync(AuthorFilterModel filterModel)
        {
            return await (filterModel.Filter == null ? _dbSet.AsNoTracking() : filterModel.Filter.ApplyFilter(_dbSet.AsNoTracking()))
                .ApplyPaginaton(filterModel.Skip, filterModel.Take)
                .ToListAsync();
        }

        public async Task<int> GetCountByFilterAsync(IQueryableMultipleResultFilter<Author> filter = null, int skip = 0)
        {
            return await (filter == null ? _dbSet.AsNoTracking() : filter.ApplyFilter(_dbSet.AsNoTracking()))
                .ApplyPaginaton(skip)
                .CountAsync();
        }

        public void Add(Author entity)
        {
            _dbSet.Add(entity);
        }

        public Author GetByFilter(IQueryableSingleResultFilter<Author> filter)
        {
            return filter.ToFunc().Invoke(_dbSet.AsNoTracking());
        }

        public void Remove(Author entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(Author entity)
        {
            _dbSet.Update(entity);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
