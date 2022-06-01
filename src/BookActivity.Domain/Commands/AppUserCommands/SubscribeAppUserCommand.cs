using System;

namespace BookActivity.Domain.Commands.AppUserCommands
{
    public sealed class SubscribeAppUserCommand : AppUserCommand
    {
        public Guid SubscribedUserId { get; set; }
        public SubscribeAppUserCommand(Guid appUserId, Guid subscribedUser)
        {
            AppUserId = appUserId;
            SubscribedUserId = subscribedUser;
        }
    }
}
