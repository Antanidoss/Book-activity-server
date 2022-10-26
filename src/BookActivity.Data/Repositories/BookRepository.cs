using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<Book>> GetByFilterAsync(Func<IQueryable<Book>, IQueryable<Book>> filterHandler)
        {
            return await filterHandler(_dbSet).ToListAsync();
        }

        public async Task<Book> GetBySpecAsync(ISpecification<Book> specification)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(specification.ToExpression());
        }

        public async Task<Book> GetBySpecAsync(ISpecification<Book> specification, params Expression<Func<Book, object>>[] includes)
        {
            return await _dbSet
                .AsNoTracking()
                .IncludeMultiple(includes)
                .FirstOrDefaultAsync(specification.ToExpression());
        }

        public async Task<IEnumerable<Book>> GetBySpecAsync(ISpecification<Book> specification, PaginationModel paginationModel, params Expression<Func<Book, object>>[] includes)
        {
            return await _dbSet
                .AsNoTracking()
                .IncludeMultiple(includes)
                .Where(specification?.ToExpression())
                .ApplyPaginaton(paginationModel.Skip, paginationModel.Take)
                .ToListAsync();
        }

        public async Task<int> GetCountBySpecAsync(ISpecification<Book> specification, int skip)
        {
            return await _dbSet.AsNoTracking()
                .Where(specification.ToExpression())
                .ApplyPaginaton(skip)
                .CountAsync();
        }

        public void Add(Book book)
        {
            _dbSet.Add(book);
        }

        public void Remove(Book book)
        {
            _dbSet.Remove(book);
        }

        public void Update(Book book)
        {
            _dbSet.Update(book);
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
