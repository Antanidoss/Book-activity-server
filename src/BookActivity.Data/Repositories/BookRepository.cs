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
    public sealed class BookRepository : IBookRepository
    {
        private readonly BookActivityContext Db;

        private readonly DbSet<Book> DbSet;
        public IUnitOfWork UnitOfWork => Db;

        public BookRepository(BookActivityContext context)
        {
            Db = context;
            DbSet = Db.Books;
        }

        public async Task<IEnumerable<Book>> GetByFilterAsync(BookFilter filter)
        {
            return await filter.ApplyFilter(DbSet).ToListAsync();
        }

        public async Task<int> GetCountByFilterAsync(BookFilter filter)
        {
            return await filter.ApplyFilter(DbSet).CountAsync();
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
