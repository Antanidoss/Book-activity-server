using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Filters;
using BookActivity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using NetDevPack.Data;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<AppUser, TResult> filterModel);
        Task<AppUser> GetByFilterAsync(DbSingleResultFilterModel<AppUser> filterModel);
        Task<int> GetCountByFilterAsync(DbMultipleResultFilterModel<AppUser> filterModel);
        Task<bool> CheckExistBySpecAsync(Specification<AppUser> specification);
        Task<IdentityResult> Addasync(AppUser user, string password);
    }
}
