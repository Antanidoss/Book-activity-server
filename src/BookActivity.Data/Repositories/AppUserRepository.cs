using Antanidoss.Specification.Filters.Interfaces;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IdentityResult> Addasync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> Updateasync(AppUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IEnumerable<AppUser>> GetByFilterAsync(AppUserFilterModel filterModel)
        {
            return await filterModel.Filter.ApplyFilter(_db.Users.AsNoTracking())
                .Skip(filterModel.Skip.Value)
                .Take(filterModel.Take.Value)
                .ToListAsync();
        }

        public AppUser GetByFilterAsync(IQueryableSingleResultFilter<AppUser> filter)
        {
            return filter.ApplyFilter(_db.Users.AsNoTracking());
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
