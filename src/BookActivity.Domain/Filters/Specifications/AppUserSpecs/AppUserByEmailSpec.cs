using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Domain.Filters.Specifications.AppUserSpecs
{
    public sealed class AppUserByEmailSpec : IQueryableFilterSpec<AppUser, string>
    {
        public IQueryable<AppUser> ApplyFilter(IQueryable<AppUser> appUsers, string email)
        {
            return appUsers.Where(x => x.Email == email);
        }
    }
}
