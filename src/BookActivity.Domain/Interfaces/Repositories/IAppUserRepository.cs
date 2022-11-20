using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<IEnumerable<AppUser>> GetBySpecAsync(ISpecification<AppUser> specification, PaginationModel paginationModel, params Expression<Func<AppUser, object>>[] includes);
        Task<AppUser> GetBySpecAsync(ISpecification<AppUser> specification, bool forAccountOperation = false, params Expression<Func<AppUser, object>>[] includes);
        Task<IdentityResult> Addasync(AppUser user, string password);
        Task<IdentityResult> UpdateAsync(AppUser user);
    }
}
