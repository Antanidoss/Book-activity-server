using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
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

        private readonly Expression<Func<AppUser, AppUser>> _baseSelectUser = a => new AppUser
        {
            Id = a.Id,
            AvatarImage = a.AvatarImage,
            UserName = a.UserName,
            Email = a.Email,
        };

        public AppUserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IUnitOfWork UnitOfWork => null;

        public async Task<IEnumerable<AppUser>> GetBySpecAsync(ISpecification<AppUser> specification, PaginationModel paginationModel, params Expression<Func<AppUser, object>>[] includes)
        {
            SpecificationValidator.ThrowExceptionIfNull(specification);
            CommonValidator.ThrowExceptionIfNull(paginationModel);

            return await _userManager.Users
                .AsNoTracking()
                .IncludeMultiple(includes)
                .Where(specification.ToExpression())
                .ApplyPaginaton(paginationModel)
                .Select(_baseSelectUser)
                .ToListAsync();
        }

        public async Task<AppUser> GetBySpecAsync(ISpecification<AppUser> specification, bool forAccountOperation, params Expression<Func<AppUser, object>>[] includes)
        {
            SpecificationValidator.ThrowExceptionIfNull(specification);

            var query = _userManager.Users
                .AsNoTracking()
                .IncludeMultiple(includes);

            if (!forAccountOperation)
                query = query.Select(_baseSelectUser);

            return await query.FirstOrDefaultAsync(specification.ToExpression());
        }

        public async Task<IdentityResult> Addasync(AppUser user, string password)
        {
            CommonValidator.ThrowExceptionIfNull(user);

            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateAsync(AppUser user)
        {
            CommonValidator.ThrowExceptionIfNull(user);

            return await _userManager.UpdateAsync(user);
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}
