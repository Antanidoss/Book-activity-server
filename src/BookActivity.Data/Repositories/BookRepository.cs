using Antanidoss.Specification.Filters.Interfaces;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class BookRepository : IBookRepository
    {
        private readonly BookActivityContext _db;

        private readonly DbSet<Book> _dbSet;

        public IUnitOfWork UnitOfWork => _db;

        public BookRepository(BookActivityContext context)
        {
            _db = context;
            _dbSet = _db.Books;
        }

        public async Task<IEnumerable<Book>> GetByFilterAsync(BookFilterModel filterModel)
        {
            return await filterModel.Filter.ApplyFilter(_dbSet.AsNoTracking())
                .ApplyPaginaton(filterModel.Skip, filterModel.Take)
                .ToListAsync();
        }

        public Book GetByFilterAsync(IQueryableSingleResultFilter<Book> filter)
        {
            return filter.ApplyFilter(_dbSet.AsNoTracking());
        }

        public async Task<int> GetCountByFilterAsync(IQueryableMultipleResultFilter<Book> filter, int skip)
        {
            return await filter.ApplyFilter(_dbSet.AsNoTracking())
                .ApplyPaginaton(skip)
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
