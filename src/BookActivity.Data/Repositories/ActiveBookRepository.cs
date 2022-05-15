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
        private readonly BookActivityContext Db;
        private readonly DbSet<ActiveBook> DbSet;
        private readonly IFilterHandler<ActiveBook, ActiveBookFilterModel> _activeBookFilterHandler;

        public IUnitOfWork UnitOfWork => Db;

        public ActiveBookRepository(BookActivityContext context, IFilterHandler<ActiveBook, ActiveBookFilterModel> activeBookFilterHandler)
        {
            Db = context;
            DbSet = Db.Set<ActiveBook>();
            _activeBookFilterHandler = activeBookFilterHandler;
        }

        public async Task<IEnumerable<ActiveBook>> GetByFilterAsync(ActiveBookFilterModel filterModel)
        {
            return await _activeBookFilterHandler
                .Handle(filterModel, DbSet.AsNoTracking())
                .ToListAsync();
        }

        public async Task<int> GetCountByFilterAsync(ActiveBookFilterModel filterModel)
        {
            return await _activeBookFilterHandler
                .Handle(filterModel, DbSet.AsNoTracking())
                .CountAsync();
        }

        public void Add(ActiveBook entity)
        {
            DbSet.Add(entity);
        }

        public void Remove(ActiveBook entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(ActiveBook entity)
        {
            DbSet.Update(entity);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}