using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.Extensions;
using BookActivity.Infrastructure.Data.Validations;
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

        public async Task<IEnumerable<Book>> GetByFilterAsync(FilterModel<Book> filter)
        {
            CommonValidator.ThrowExceptionIfNull(filter);

            var query = _dbSet.AsNoTracking().IncludeMultiple(filter.Includes);

            return await filter.FilterHandler(query)
                .ToListAsync();
        }

        public async Task<Book> GetBySpecAsync(ISpecification<Book> specification)
        {
            SpecificationValidator.ThrowExceptionIfNull(specification);

            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(specification.ToExpression());
        }

        public async Task<Book> GetBySpecAsync(ISpecification<Book> specification, params Expression<Func<Book, object>>[] includes)
        {
            SpecificationValidator.ThrowExceptionIfNull(specification);

            return await _dbSet
                .AsNoTracking()
                .IncludeMultiple(includes)
                .FirstOrDefaultAsync(specification.ToExpression());
        }

        public async Task<IEnumerable<Book>> GetBySpecAsync(ISpecification<Book> specification, PaginationModel paginationModel, params Expression<Func<Book, object>>[] includes)
        {
            SpecificationValidator.ThrowExceptionIfNull(specification);
            CommonValidator.ThrowExceptionIfNull(paginationModel);

            return await _dbSet
                .AsNoTracking()
                .IncludeMultiple(includes)
                .Where(specification?.ToExpression())
                .ApplyPaginaton(paginationModel.Skip, paginationModel.Take)
                .ToListAsync();
        }

        public async Task<int> GetCountByFilterAsync(Func<IQueryable<Book>, IQueryable<Book>> filterHandler, int skip)
        {
            CommonValidator.ThrowExceptionIfNull(filterHandler);

            return await filterHandler(_dbSet.AsNoTracking())
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
