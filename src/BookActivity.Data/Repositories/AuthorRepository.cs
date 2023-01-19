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
using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.Validations;

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
            CommonValidator.ThrowExceptionIfNull(filterHandler);

            return await filterHandler(_dbSet).ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetBySpecAsync(Specification<Author> specification, PaginationModel paginationModel)
        {
            SpecificationValidator.ThrowExceptionIfNull(specification);
            CommonValidator.ThrowExceptionIfNull(paginationModel);

            return await _dbSet
                .AsNoTracking()
                .Where(specification)
                .ApplyPaginaton(paginationModel)
                .ToListAsync();
        }

        public async Task<Author> GetBySpecAsync(Specification<Author> specification)
        {
            SpecificationValidator.ThrowExceptionIfNull(specification);

            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(specification);
        }

        public async Task<int> GetCountBySpecAsync(Specification<Author> specification, int skip = 0)
        {
            SpecificationValidator.ThrowExceptionIfNull(specification);

            return await _dbSet
                .AsNoTracking()
                .ApplyPaginaton(skip)
                .Where(specification)
                .CountAsync();
        }

        public void Add(Author entity)
        {
            CommonValidator.ThrowExceptionIfNull(entity);

            _dbSet.Add(entity);
        }

        public void Remove(Author entity)
        {
            CommonValidator.ThrowExceptionIfNull(entity);

            _dbSet.Remove(entity);
        }

        public void Update(Author entity)
        {
            CommonValidator.ThrowExceptionIfNull(entity);

            _dbSet.Update(entity);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
