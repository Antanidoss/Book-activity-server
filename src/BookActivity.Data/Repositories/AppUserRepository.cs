using Antanidoss.Specification.Filters.Interfaces;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookActivity.Infrastructure.Data.Helpers;

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

        public async Task<IEnumerable<AppUser>> GetByFilterAsync(AppUserFilterModel filterModel)
        {
            return await filterModel.Filter.ApplyFilter(_userManager.Users.AsNoTracking())
                .ApplyPaginaton(filterModel.Skip, filterModel.Take)
                .ToListAsync();
        }

        public AppUser GetByFilter(IQueryableSingleResultFilter<AppUser> filter)
        {
            return filter.ApplyFilter(_userManager.Users);
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
