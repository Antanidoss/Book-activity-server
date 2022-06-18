using BookActivity.Domain.Filters.Models;
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
                .Skip(filterModel.Skip.Value)
                .Take(filterModel.Take.Value)
                .ToListAsync();
        }

        public async Task<int> GetCountByFilterAsync(BookAuthorFilterModel filterModel)
        {
            return await filterModel.Filter.ApplyFilter(_dbSet.AsNoTracking())
                .Skip(filterModel.Skip.Value)
                .CountAsync();
        }

        public void Add(BookAuthor entity)
        {
            _dbSet.Add(entity);
        }

        public async Task<BookAuthor> GetByAsync(Expression<Func<BookAuthor, bool>> condition)
        {
            return await _dbSet.FirstOrDefaultAsync(condition);
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
