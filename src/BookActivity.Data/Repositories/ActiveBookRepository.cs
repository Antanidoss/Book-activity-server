using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    public class ActiveBookRepository : IActiveBookRepository
    {
        protected readonly BookActivityContext Db;
        protected readonly DbSet<ActiveBook> DbSet;

        public ActiveBookRepository(BookActivityContext context)
        {
            Db = context;
            DbSet = Db.Set<ActiveBook>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<ActiveBook> GetByAsync(Expression<Func<ActiveBook, bool>> condition)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(condition);
        }

        public async Task<IEnumerable<ActiveBook>> GetByAsync(Expression<Func<ActiveBook, bool>> condition, int skip, int take)
        {
            return await DbSet.AsNoTracking().Where(condition).Skip(skip).Take(take).ToListAsync();
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