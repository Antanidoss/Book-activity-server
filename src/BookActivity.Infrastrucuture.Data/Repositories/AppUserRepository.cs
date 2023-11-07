using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.EF;
using BookActivity.Infrastructure.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        private DbSet<AppUser> _dbSet;

        private BookActivityContext _db;

        public AppUserRepository(UserManager<AppUser> userManager, BookActivityContext context)
        {
            _db = context;
            _userManager = userManager;
            _dbSet = context.Users;
        }

        public IUnitOfWork UnitOfWork => _db;

        public async Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<AppUser, TResult> filterModel)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = _dbSet.IncludeMultiple(filterModel.Includes);

            if (!filterModel.ForUpdate)
                query = query.AsNoTracking();

            if (typeof(TResult) == typeof(IEnumerable<ActiveBook>))
                query = query.ApplyPaginaton(filterModel.PaginationModel);

            return await filterModel.Filter(query);
        }

        public async Task<AppUser> GetByFilterAsync(DbSingleResultFilterModel<AppUser> filterModel)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = _dbSet.IncludeMultiple(filterModel.Includes);

            if (!filterModel.ForUpdate)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(filterModel.Specification);
        }

        public async Task<int> GetCountByFilterAsync(DbMultipleResultFilterModel<AppUser> filterModel)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = await filterModel.Filter(_dbSet);

            return await query
                .ApplyPaginaton(filterModel.PaginationModel)
                .CountAsync();
        }

        public async Task<bool> CheckExistBySpecAsync(Specification<AppUser> specification)
        {
            return await _dbSet.AnyAsync(specification);
        }

        public async Task<IdentityResult> Addasync(AppUser user, string password)
        {
            CommonValidator.ThrowExceptionIfNull(user);

            return await _userManager.CreateAsync(user, password);
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}
