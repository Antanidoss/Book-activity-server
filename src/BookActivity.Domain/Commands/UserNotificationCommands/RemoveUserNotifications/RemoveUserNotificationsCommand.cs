using System;

namespace BookActivity.Domain.Commands.UserNotificationCommands.RemoveUserNotifications
{
    public sealed class RemoveUserNotificationsCommand : Command
    {
        public readonly Guid[] UserNotificationIds;

        public RemoveUserNotificationsCommand(params Guid[] userNotificationIds)
        {
            UserNotificationIds = userNotificationIds;
        }
    }
}
