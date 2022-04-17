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
    public class AuthorRepository : IAuthorRepository
    {
        protected readonly BookActivityContext Db;

        protected readonly DbSet<BookAuthor> DbSet;
        public IUnitOfWork UnitOfWork => Db;

        public AuthorRepository(BookActivityContext context)
        {
            Db = context;
            DbSet = Db.Set<BookAuthor>();
        }

        public void Add(BookAuthor entity)
        {
            DbSet.Add(entity);
        }

        public async Task<BookAuthor> GetByAsync(Expression<Func<BookAuthor, bool>> condition)
        {
            return await DbSet.FirstOrDefaultAsync(condition);
        }

        public async Task<IEnumerable<BookAuthor>> GetByAsync(Expression<Func<BookAuthor, bool>> condition, int skip, int take)
        {
            return await DbSet
               .Where(condition)
               .Skip(skip)
               .Take(take)
               .ToListAsync();
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
