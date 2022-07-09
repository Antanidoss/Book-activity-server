using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Models;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.AppUserSpecs
{
    public sealed class AppUserByIdSpec : ISpecification<AppUser>
    {
        private readonly Guid _appUserId;

        public AppUserByIdSpec(Guid appUserId)
        {
            _appUserId = appUserId;
        }

        public Expression<Func<AppUser, bool>> ToExpression()
        {
            return a => a.Id == _appUserId;
        }
    }
}
