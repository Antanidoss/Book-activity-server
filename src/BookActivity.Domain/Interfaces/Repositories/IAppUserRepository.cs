using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<IdentityResult> Addasync(AppUser user, string password);
        Task<IEnumerable<AppUser>> GetByFilterAsync(AppUserFilterModel filterModel);
    }
}
