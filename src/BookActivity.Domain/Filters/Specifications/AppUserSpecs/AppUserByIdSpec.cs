using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System;
using System.Linq;

namespace BookActivity.Domain.Filters.Specifications.AppUserSpecs
{
    public sealed class AppUserByIdSpec : IQueryableFilterSpec<AppUser, Guid>
    {
        public IQueryable<AppUser> ApplyFilter(IQueryable<AppUser> appUsers, Guid appUserId)
        {
            return appUsers.Where(a => a.Id == appUserId);
        }
    }
}
