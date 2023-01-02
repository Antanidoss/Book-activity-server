using Antanidoss.Specification.Interfaces;
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
        Task<IEnumerable<AppUser>> GetBySpecAsync(ISpecification<AppUser> specification, PaginationModel paginationModel, params Expression<Func<AppUser, object>>[] includes);
        Task<AppUser> GetBySpecAsync(ISpecification<AppUser> specification, bool forAccountOperation = false, params Expression<Func<AppUser, object>>[] includes);
        Task<AppUser> GetForUpdateBySpecAsync(ISpecification<AppUser> specification, bool forAccountOperation = false, params Expression<Func<AppUser, object>>[] includes);
        Task<int> GetCountByFilterAsync(Func<IQueryable<AppUser>, IQueryable<AppUser>> filter, int skip = PaginationModel.SkipDefault);
        Task<bool> CheckExistBySpecAsync(ISpecification<AppUser> specification);
        Task<IdentityResult> Addasync(AppUser user, string password);
        Task<IdentityResult> UpdateAccountDataAsync(AppUser user);
        void Update(AppUser appUser);
    }
}
