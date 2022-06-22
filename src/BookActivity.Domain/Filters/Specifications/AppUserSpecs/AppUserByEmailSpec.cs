using BookActivity.Domain.Models;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Filters.Specifications.AppUserSpecs
{
    public sealed class AppUserByEmailSpec : IQueryableFilterSpec<AppUser>
    {
        private readonly string _email;

        public AppUserByEmailSpec(string email)
        {
            _email = email;
        }

        public IQueryable<AppUser> ApplyFilter(IQueryable<AppUser> appUsers)
        {
            return appUsers.Where(ToExpression());
        }

        public Expression<Func<AppUser, bool>> ToExpression()
        {
            return x => x.Email == _email;
        }
    }
}
