using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<AppUser, TResult> filterModel, CancellationToken cancellationToken = default);
        Task<AppUser> GetByFilterAsync(DbSingleResultFilterModel<AppUser> filterModel, CancellationToken cancellationToken = default);
        Task<int> GetCountByFilterAsync(DbMultipleResultFilterModel<AppUser> filterModel, CancellationToken cancellationToken = default);
        Task<bool> CheckExistBySpecAsync(Specification<AppUser> specification, CancellationToken cancellationToken = default);
        Task<IdentityResult> Addasync(AppUser user, string password, CancellationToken cancellationToken = default);
    }
}
