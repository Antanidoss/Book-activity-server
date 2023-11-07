using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Commands.UserNotificationCommands.RemoveUserNotifications
{
    public sealed class RemoveUserNotificationsCommand : Command
    {
        public readonly IEnumerable<Guid> UserNotificationIds;

        public RemoveUserNotificationsCommand(IEnumerable<Guid> userNotificationIds)
        {
            UserNotificationIds = userNotificationIds;
        }
    }
}
