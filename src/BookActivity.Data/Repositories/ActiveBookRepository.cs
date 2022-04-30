using BookActivity.Domain.Filters.FilterFacades;
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
        public IUnitOfWork UnitOfWork => Db;

        public ActiveBookRepository(BookActivityContext context)
        {
            Db = context;
            DbSet = Db.Set<ActiveBook>();
        }

        public async Task<IEnumerable<ActiveBook>> GetByFilterAsync(BaseFilter<ActiveBook> filter)
        {
            return await filter.ApplyFilter(DbSet).ToListAsync();
        }

        public async Task<int> GetCountByFilterAsync(BaseFilter<ActiveBook> filter)
        {
            return await filter.ApplyFilter(DbSet).CountAsync();
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