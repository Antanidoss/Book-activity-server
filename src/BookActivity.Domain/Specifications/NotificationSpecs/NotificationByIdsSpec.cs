using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.NotificationSpecs
{
    internal class NotificationByIdsSpec : Specification<Notification>
    {
        private readonly IEnumerable<Guid> _userNotificationIds;

        public NotificationByIdsSpec(IEnumerable<Guid> userNotificationIds)
        {
            _userNotificationIds = userNotificationIds;
        }

        public override Expression<Func<Notification, bool>> ToExpression()
        {
            return n => _userNotificationIds.Contains(n.Id);
        }
    }
}
