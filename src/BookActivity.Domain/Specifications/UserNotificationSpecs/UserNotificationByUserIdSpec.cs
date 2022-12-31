using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Models;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.UserNotificationSpecs
{
    public sealed class UserNotificationByUserIdSpec : ISpecification<UserNotification>
    {
        private readonly Guid _userId;

        public UserNotificationByUserIdSpec(Guid userId)
        {
            _userId = userId;
        }

        public Expression<Func<UserNotification, bool>> ToExpression()
        {
            return u => u.UserId == _userId;
        }
    }
}
