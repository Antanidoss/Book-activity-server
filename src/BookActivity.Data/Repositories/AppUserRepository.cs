using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Extensions;
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

        public AppUserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IUnitOfWork UnitOfWork => null;

        public async Task<IEnumerable<AppUser>> GetBySpecAsync(ISpecification<AppUser> specification, PaginationModel paginationModel, params Expression<Func<AppUser, object>>[] includes)
        {
            return await _userManager.Users
                .AsNoTracking()
                .IncludeMultiple(includes)
                .Where(specification.ToExpression())
                .ApplyPaginaton(paginationModel)
                .ToListAsync();
        }

        public async Task<AppUser> GetBySpecAsync(ISpecification<AppUser> specification, params Expression<Func<AppUser, object>>[] includes)
        {
            return await _userManager.Users
                .AsNoTracking()
                .IncludeMultiple(includes)
                .FirstOrDefaultAsync(specification.ToExpression());
        }

        public async Task<IdentityResult> Addasync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateAsync(AppUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}
