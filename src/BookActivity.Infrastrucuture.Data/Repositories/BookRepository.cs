using BookActivity.Domain.Filters;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.Extensions;
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

        public IUnitOfWork UnitOfWork => _db;

        public BookRepository(BookActivityContext context)
        {
            _db = context;
            _dbSet = _db.Books;
        }

        public async Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<Book, TResult> filterModel)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = _dbSet.IncludeMultiple(filterModel.Includes);

            if (!filterModel.ForUpdate)
                query = query.AsNoTracking();

            if (typeof(TResult) == typeof(IEnumerable<Book>))
                query = query.ApplyPaginaton(filterModel.PaginationModel);

            return await filterModel.Filter(query);
        }

        public async Task<Book> GetByFilterAsync(DbSingleResultFilterModel<Book> filterModel)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = _dbSet.IncludeMultiple(filterModel.Includes);

            if (!filterModel.ForUpdate)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(filterModel.Specification);
        }

        public async Task<int> GetCountByFilterAsync(DbMultipleResultFilterModel<Book> filterModel)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = await filterModel.Filter(_dbSet);

            return await query
                .ApplyPaginaton(filterModel.PaginationModel)
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

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
