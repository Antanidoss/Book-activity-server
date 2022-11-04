using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Filters.Models;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class AuthorRepository : IAuthorRepository
    {
        private readonly BookActivityContext _db;

        private readonly DbSet<Author> _dbSet;

        public IUnitOfWork UnitOfWork => _db;

        public AuthorRepository(BookActivityContext context)
        {
            _db = context;
            _dbSet = _db.Set<Author>();
        }

        public async Task<IEnumerable<Author>> GetByFilterAsync(Func<IQueryable<Author>, IQueryable<Author>> filterHandler)
        {
            return await filterHandler(_dbSet).ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetBySpecAsync(ISpecification<Author> specification, PaginationModel paginationModel)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(specification.ToExpression())
                .ApplyPaginaton(paginationModel)
                .ToListAsync();
        }

        public async Task<Author> GetBySpecAsync(ISpecification<Author> specification)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(specification.ToExpression());
        }

        public async Task<int> GetCountBySpecAsync(ISpecification<Author> specification, int skip = 0)
        {
            return await _dbSet
                .AsNoTracking()
                .ApplyPaginaton(skip)
                .Where(specification.ToExpression())
                .CountAsync();
        }

        public void Add(Author entity)
        {
            _dbSet.Add(entity);
        }


        public void Remove(Author entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(Author entity)
        {
            _dbSet.Update(entity);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
