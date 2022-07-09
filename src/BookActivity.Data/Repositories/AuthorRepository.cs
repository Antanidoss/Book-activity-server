using Antanidoss.Specification.Filters.Interfaces;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class AuthorRepository : IAuthorRepository
    {
        private readonly BookActivityContext _db;

        private readonly DbSet<BookAuthor> _dbSet;

        public IUnitOfWork UnitOfWork => _db;

        public AuthorRepository(BookActivityContext context)
        {
            _db = context;
            _dbSet = _db.Set<BookAuthor>();
        }

        public async Task<IEnumerable<BookAuthor>> GetByFilterAsync(BookAuthorFilterModel filterModel)
        {
            return await filterModel.Filter.ApplyFilter(_dbSet.AsNoTracking())
                .ApplyPaginaton(filterModel.Skip, filterModel.Take)
                .ToListAsync();
        }

        public async Task<int> GetCountByFilterAsync(IQueryableMultipleResultFilter<BookAuthor> filter, int skip = 0)
        {
            return await filter.ApplyFilter(_dbSet)
                .ApplyPaginaton(skip)
                .CountAsync();
        }

        public void Add(BookAuthor entity)
        {
            _dbSet.Add(entity);
        }

        public BookAuthor GetByFilter(IQueryableSingleResultFilter<BookAuthor> filter)
        {
            return filter.ToFunc().Invoke(_dbSet);
        }

        public void Remove(BookAuthor entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(BookAuthor entity)
        {
            _dbSet.Update(entity);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
