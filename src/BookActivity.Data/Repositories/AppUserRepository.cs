using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.Extensions;
using BookActivity.Infrastructure.Data.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        private DbSet<AppUser> _dbSet;

        private BookActivityContext _db;

        private readonly Expression<Func<AppUser, AppUser>> _baseSelectUser = a => new AppUser
        {
            Id = a.Id,
            AvatarImage = a.AvatarImage,
            UserName = a.UserName,
            Email = a.Email,
        };

        public AppUserRepository(UserManager<AppUser> userManager, BookActivityContext context)
        {
            _db = context;
            _userManager = userManager;
            _dbSet = context.Users;
        }

        public IUnitOfWork UnitOfWork => _db;

        public async Task<TResult> GetByFilterAsync<TResult>(Func<IQueryable<AppUser>, Task<TResult>> filter, params Expression<Func<AppUser, object>>[] includes)
        {
            CommonValidator.ThrowExceptionIfNull(filter);

            var query = _userManager.Users.AsNoTracking().IncludeMultiple(includes);

            return await filter(query);
        }

        public async Task<IEnumerable<AppUser>> GetBySpecAsync(Specification<AppUser> specification, PaginationModel paginationModel, params Expression<Func<AppUser, object>>[] includes)
        {
            SpecificationValidator.ThrowExceptionIfNull(specification);
            CommonValidator.ThrowExceptionIfNull(paginationModel);

            return await _dbSet
                .AsNoTracking()
                .IncludeMultiple(includes)
                .Where(specification)
                .ApplyPaginaton(paginationModel)
                .Select(_baseSelectUser)
                .ToListAsync();
        }

        public async Task<AppUser> GetBySpecAsync(Specification<AppUser> specification, bool forAccountOperation, params Expression<Func<AppUser, object>>[] includes)
        {
            SpecificationValidator.ThrowExceptionIfNull(specification);

            var query = _dbSet.IncludeMultiple(includes);

            if (!forAccountOperation)
                query = query.Select(_baseSelectUser);

            return await query.AsNoTracking().FirstOrDefaultAsync(specification);
        }

        public async Task<AppUser> GetForUpdateBySpecAsync(Specification<AppUser> specification, bool forAccountOperation = false, params Expression<Func<AppUser, object>>[] includes)
        {
            SpecificationValidator.ThrowExceptionIfNull(specification);

            var query = _dbSet.IncludeMultiple(includes);

            if (!forAccountOperation)
                query = query.Select(_baseSelectUser);

            return await query.FirstOrDefaultAsync(specification);
        }

        public async Task<int> GetCountByFilterAsync(Func<IQueryable<AppUser>, IQueryable<AppUser>> filter, int skip = 0)
        {
            CommonValidator.ThrowExceptionIfNull(filter);

            return await filter(_userManager.Users).CountAsync();
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

        public async Task<IdentityResult> UpdateAccountDataAsync(AppUser user)
        {
            CommonValidator.ThrowExceptionIfNull(user);

            return await _userManager.UpdateAsync(user);
        }

        public void Update(AppUser appUser)
        {
            _db.Update(appUser);
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}
