using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<ActiveBook>> GetByFilterAsync(ActiveBookFilterModel filterModel)
        {
            return await filterModel.Filter.ApplyFilter(_dbSet.AsNoTracking())
                .Skip(filterModel.Skip)
                .Take(filterModel.Take)
                .ToListAsync();
        }

        public async Task<int> GetCountByFilterAsync(ActiveBookFilterModel filterModel)
        {
            return await filterModel.Filter.ApplyFilter(_dbSet.AsNoTracking())
                .Skip(filterModel.Skip)
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