using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.UserNotificationSpecs
{
    internal class UserNotificationByIdsSpec : Specification<UserNotification>
    {
        private readonly IEnumerable<Guid> _userNotificationIds;

        public UserNotificationByIdsSpec(IEnumerable<Guid> userNotificationIds)
        {
            _userNotificationIds = userNotificationIds;
        }

        public override Expression<Func<UserNotification, bool>> ToExpression()
        {
            return n => _userNotificationIds.Contains(n.Id);
        }
    }
}
