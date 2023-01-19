using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Models;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.AppUserSpecs
{
    public sealed class AppUserByIdSpec : Specification<AppUser>
    { 
        private readonly Guid _appUserId;

        public AppUserByIdSpec(Guid appUserId)
        {
            _appUserId = appUserId;
        }

        public override Expression<Func<AppUser, bool>> ToExpression()
        {
            return a => a.Id == _appUserId;
        }
    }
}
