using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Filters.Specifications.AppUserSpecs
{
    public sealed class AppUserByIdSpec : IQueryableFilterSpec<AppUser>
    {
        private readonly Guid _appUserId;

        public AppUserByIdSpec(Guid appUserId)
        {
            _appUserId = appUserId;
        }

        public IQueryable<AppUser> ApplyFilter(IQueryable<AppUser> appUsers)
        {
            return appUsers.Where(ToExpression());
        }

        public Expression<Func<AppUser, bool>> ToExpression()
        {
            return a => a.Id == _appUserId;
        }
    }
}
