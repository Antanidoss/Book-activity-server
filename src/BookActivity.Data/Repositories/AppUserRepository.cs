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
        private readonly BookActivityContext _db;

        private readonly UserManager<AppUser> _userManager;

        public AppUserRepository(BookActivityContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IUnitOfWork UnitOfWork => _db;

        public async Task<IEnumerable<AppUser>> GetByFilterAsync(AppUserFilterModel filterModel)
        {
            return await filterModel.Filter.ApplyFilter(_db.Users.AsNoTracking())
                .ApplyPaginaton(filterModel.Skip, filterModel.Take)
                .ToListAsync();
        }

        public AppUser GetByFilterAsync(IQueryableSingleResultFilter<AppUser> filter)
        {
            return filter.ApplyFilter(_db.Users.AsNoTracking());
        }

        public async Task<IdentityResult> Addasync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task UpdateAsync(AppUser user)
        {
            _db.Users.Update(user);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
