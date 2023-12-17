using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Models;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.UserNotificationSpecs
{
    public sealed class UserNotificationByUserIdSpec : Specification<UserNotification>
    {
        private readonly Guid _userId;

        public UserNotificationByUserIdSpec(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<UserNotification, bool>> ToExpression()
        {
            return u => u.ToUserId == _userId;
        }
    }
}
