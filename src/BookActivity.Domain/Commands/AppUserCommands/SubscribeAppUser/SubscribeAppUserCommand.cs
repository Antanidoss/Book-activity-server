using System;

namespace BookActivity.Domain.Commands.AppUserCommands.SubscribeAppUser
{
    public sealed class SubscribeAppUserCommand : AppUserCommand
    {
        public Guid UserIdWhoSubscribed { get; set; }
        public Guid SubscribedUserId { get; set; }
        public SubscribeAppUserCommand(Guid subscribedUserId, Guid userIdWhoSubscribed)
        {
            SubscribedUserId = subscribedUserId;
            UserIdWhoSubscribed = userIdWhoSubscribed;
        }
    }
}
