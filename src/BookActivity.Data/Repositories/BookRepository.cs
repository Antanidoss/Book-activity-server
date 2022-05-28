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
    internal sealed class BookRepository : IBookRepository
    {
        private readonly BookActivityContext _db;
        private readonly DbSet<Book> _dbSet;
        private readonly IFilterHandler<Book, BookFilterModel> _bookFilterHandler;

        public IUnitOfWork UnitOfWork => _db;

        public BookRepository(BookActivityContext context, IFilterHandler<Book, BookFilterModel> bookFilterHandler)
        {
            _db = context;
            _dbSet = _db.Books;
            _bookFilterHandler = bookFilterHandler;
        }

        public async Task<IEnumerable<Book>> GetByFilterAsync(BookFilterModel filterModel)
        {
            return await _bookFilterHandler
                .Handle(filterModel, _dbSet.AsNoTracking())
                .ToListAsync();
        }

        public async Task<int> GetCountByFilterAsync(BookFilterModel filterModel)
        {
            return await _bookFilterHandler
                .Handle(filterModel, _dbSet.AsNoTracking())
                .CountAsync();
        }

        public void Add(Book entity)
        {
            _dbSet.Add(entity);
        }

        public void Remove(Book entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(Book entity)
        {
            _dbSet.Update(entity);
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
