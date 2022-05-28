using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Application.Implementation.FilterHandlers
{
    public sealed class AppUserFilterHandler : IFilterHandler<AppUser, AppUserFilterModel>
    {
        public IQueryable<AppUser> Handle(AppUserFilterModel filterModel, IQueryable<AppUser> appUsers)
        {
            if (appUsers == null) return null;

            if (filterModel.AppUserId != null)
                appUsers = filterModel.AppUserId.FilterSpec.ApplyFilter(appUsers, filterModel.AppUserId.Value);

            if (filterModel.Email != null)
                appUsers = filterModel.Email.FilterSpec.ApplyFilter(appUsers, filterModel.Email.Value);

            return appUsers.OrderBy(u => u.Id).Skip(filterModel.Skip).Take(filterModel.Take);
        }
    }
}
