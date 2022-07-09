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

        public ActiveBook GetByFilterAsync(IQueryableSingleResultFilter<ActiveBook> filter)
        {
            return filter.ApplyFilter(_dbSet.AsNoTracking());
        }

        public async Task<IEnumerable<ActiveBook>> GetByFilterAsync(ActiveBookFilterModel activeBookFilterModel)
        {
            return await activeBookFilterModel.Filter.ApplyFilter(_dbSet.AsNoTracking())
                .ApplyPaginaton(activeBookFilterModel.Skip, activeBookFilterModel.Take)
                .ToListAsync();
        }

        public async Task<int> GetCountByFilterAsync(IQueryableMultipleResultFilter<ActiveBook> filter, int skip = 0)
        {
            return await filter.ApplyFilter(_dbSet)
                .ApplyPaginaton(skip)
                .CountAsync();
        }

        public void Add(ActiveBook entity)
        {
            _dbSet.Add(entity);
        }

        public void Remove(ActiveBook entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(ActiveBook entity)
        {
            _dbSet.Update(entity);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}