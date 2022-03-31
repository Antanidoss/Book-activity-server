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
    public class BookRepository : IBookRepository
    {
        protected readonly BookActivityContext Db;

        protected readonly DbSet<Book> DbSet;
        public IUnitOfWork UnitOfWork => Db;

        public BookRepository(BookActivityContext context)
        {
            Db = context;
            DbSet = Db.Books;
        }

        public async Task<Book> GetByAsync(Expression<Func<Book, bool>> condition)
        {
            return await DbSet
                .Where(condition)
                .Include(b => b.BookOpinions)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetByAsync(Expression<Func<Book, bool>> condition, int skip, int take)
        {
            return await DbSet
                .Skip(skip)
                .Take(take)
                .Where(condition)
                .Include(b => b.BookOpinions)
                .ToListAsync();
        }

        public void Add(Book entity)
        {
            DbSet.Add(entity);
        }

        public void Remove(Book entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(Book entity)
        {
            DbSet.Update(entity);
        }
        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
