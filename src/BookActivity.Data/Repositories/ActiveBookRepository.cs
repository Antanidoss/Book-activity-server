using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    public sealed class ActiveBookRepository : IActiveBookRepository
    {
        private readonly BookActivityContext _db;
        private readonly DbSet<ActiveBook> _dbSet;
        private readonly IFilterHandler<ActiveBook, ActiveBookFilterModel> _activeBookFilterHandler;

        public IUnitOfWork UnitOfWork => _db;

        public ActiveBookRepository(BookActivityContext context, IFilterHandler<ActiveBook, ActiveBookFilterModel> activeBookFilterHandler)
        {
            _db = context;
            _dbSet = _db.Set<ActiveBook>();
            _activeBookFilterHandler = activeBookFilterHandler;
        }

        public async Task<IEnumerable<ActiveBook>> GetByFilterAsync(ActiveBookFilterModel filterModel)
        {
            return await _activeBookFilterHandler
                .Handle(filterModel, _dbSet.AsNoTracking())
                .ToListAsync();
        }

        public async Task<int> GetCountByFilterAsync(ActiveBookFilterModel filterModel)
        {
            return await _activeBookFilterHandler
                .Handle(filterModel, _dbSet.AsNoTracking())
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