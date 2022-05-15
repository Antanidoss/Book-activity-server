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
    public sealed class BookRepository : IBookRepository
    {
        private readonly BookActivityContext Db;
        private readonly DbSet<Book> DbSet;
        private readonly IFilterHandler<Book, BookFilterModel> _bookFilterHandler;

        public IUnitOfWork UnitOfWork => Db;

        public BookRepository(BookActivityContext context, IFilterHandler<Book, BookFilterModel> bookFilterHandler)
        {
            Db = context;
            DbSet = Db.Books;
            _bookFilterHandler = bookFilterHandler;
        }

        public async Task<IEnumerable<Book>> GetByFilterAsync(BookFilterModel filterModel)
        {
            return await _bookFilterHandler
                .Handle(filterModel, DbSet.AsNoTracking())
                .ToListAsync();
        }

        public async Task<int> GetCountByFilterAsync(BookFilterModel filterModel)
        {
            return await _bookFilterHandler
                .Handle(filterModel, DbSet.AsNoTracking())
                .CountAsync();
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
