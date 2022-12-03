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
    internal sealed class ActiveBookRepository : IActiveBookRepository
    {
        private readonly BookActivityContext _db;

        private readonly DbSet<ActiveBook> _dbSet;

        public IUnitOfWork UnitOfWork => _db;

        public ActiveBookRepository(BookActivityContext context)
        {
            _db = context;
            _dbSet = _db.Set<ActiveBook>();
        }

        public async Task<TResult> GetByFilterAsync<TResult>(Func<IQueryable<ActiveBook>, Task<TResult>> filter, params Expression<Func<ActiveBook, object>>[] includes)
        {
            CommonValidator.ThrowExceptionIfNull(filter);

            var query = _dbSet.AsNoTracking().IncludeMultiple(includes);

            return await filter(query);
        }

        public async Task<ActiveBook> GetBySpecAsync(ISpecification<ActiveBook> specification, params Expression<Func<ActiveBook, object>>[] includes)
        {
            SpecificationValidator.ThrowExceptionIfNull(specification);

            return await _dbSet
                .AsNoTracking()
                .IncludeMultiple(includes)
                .FirstOrDefaultAsync(specification.ToExpression());
        }

        public async Task<IEnumerable<ActiveBook>> GetBySpecAsync(ISpecification<ActiveBook> specification, PaginationModel paginationModel, params Expression<Func<ActiveBook, object>>[] includes)
        {
            SpecificationValidator.ThrowExceptionIfNull(specification);

            return await _dbSet
                .AsNoTracking()
                .IncludeMultiple(includes)
                .Where(specification.ToExpression())
                .ApplyPaginaton(paginationModel)
                .ToListAsync();
        }

        public async Task<int> GetCountByFilterAsync(Func<IQueryable<ActiveBook>, IQueryable<ActiveBook>> filterHandler, int skip = 0)
        {
            CommonValidator.ThrowExceptionIfNull(filterHandler);

            return await filterHandler(_dbSet.AsNoTracking())
                .ApplyPaginaton(skip)
                .CountAsync();
        }

        public void Add(ActiveBook activeBook)
        {
            CommonValidator.ThrowExceptionIfNull(activeBook);

            _dbSet.Add(activeBook);
        }

        public void Remove(ActiveBook activeBook)
        {
            CommonValidator.ThrowExceptionIfNull(activeBook);

            _dbSet.Remove(activeBook);
        }

        public void Update(ActiveBook activeBook)
        {
            CommonValidator.ThrowExceptionIfNull(activeBook);

            _dbSet.Update(activeBook);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}