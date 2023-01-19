using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<TResult> GetByFilterAsync<TResult>(Func<IQueryable<AppUser>, Task<TResult>> filter, params Expression<Func<AppUser, object>>[] includes);
        Task<IEnumerable<AppUser>> GetBySpecAsync(Specification<AppUser> specification, PaginationModel paginationModel, params Expression<Func<AppUser, object>>[] includes);
        Task<AppUser> GetBySpecAsync(Specification<AppUser> specification, bool forAccountOperation = false, params Expression<Func<AppUser, object>>[] includes);
        Task<AppUser> GetForUpdateBySpecAsync(Specification<AppUser> specification, bool forAccountOperation = false, params Expression<Func<AppUser, object>>[] includes);
        Task<int> GetCountByFilterAsync(Func<IQueryable<AppUser>, IQueryable<AppUser>> filter, int skip = PaginationModel.SkipDefault);
        Task<bool> CheckExistBySpecAsync(Specification<AppUser> specification);
        Task<IdentityResult> Addasync(AppUser user, string password);
        Task<IdentityResult> UpdateAccountDataAsync(AppUser user);
        void Update(AppUser appUser);
    }
}
