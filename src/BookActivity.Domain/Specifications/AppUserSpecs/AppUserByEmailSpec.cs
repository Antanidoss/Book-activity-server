using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.AppUserSpecs
{
    public sealed class AppUserByEmailSpec : Specification<AppUser>
    {
        private readonly string _email;

        public AppUserByEmailSpec(string email)
        {
            _email = email;
        }

        public override Expression<Func<AppUser, bool>> ToExpression()
        {
            return x => x.Email == _email;
        }
    }
}
