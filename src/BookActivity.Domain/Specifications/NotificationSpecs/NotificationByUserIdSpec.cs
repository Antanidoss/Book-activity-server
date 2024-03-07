using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Models;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.NotificationSpecs
{
    public sealed class NotificationByUserIdSpec : Specification<Notification>
    {
        private readonly Guid _userId;

        public NotificationByUserIdSpec(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<Notification, bool>> ToExpression()
        {
            return u => u.ToUserId == _userId;
        }
    }
}
