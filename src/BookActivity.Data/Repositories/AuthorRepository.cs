using BookActivity.Domain.Filters.FilterFacades;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    public sealed class AuthorRepository : IAuthorRepository
    {
        private readonly BookActivityContext Db;

        private readonly DbSet<BookAuthor> DbSet;
        public IUnitOfWork UnitOfWork => Db;

        public AuthorRepository(BookActivityContext context)
        {
            Db = context;
            DbSet = Db.Set<BookAuthor>();
        }

        public async Task<IEnumerable<BookAuthor>> GetByFilterAsync(BookAuthorFilter filter)
        {
            return await filter.ApplyFilter(DbSet).ToListAsync();
        }

        public async Task<int> GetCountByFilterAsync(BookAuthorFilter filter)
        {
            return await filter.ApplyFilter(DbSet).CountAsync();
        }

        public void Add(BookAuthor entity)
        {
            DbSet.Add(entity);
        }

        public async Task<BookAuthor> GetByAsync(Expression<Func<BookAuthor, bool>> condition)
        {
            return await DbSet.FirstOrDefaultAsync(condition);
        }

        public void Remove(BookAuthor entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(BookAuthor entity)
        {
            DbSet.Update(entity);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
