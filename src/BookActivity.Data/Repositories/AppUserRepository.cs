using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Identity;
using NetDevPack.Data;
using System;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    public sealed class AppUserRepository : IAppUserRepository
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

        public async Task<AppUser> FindByIdAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
