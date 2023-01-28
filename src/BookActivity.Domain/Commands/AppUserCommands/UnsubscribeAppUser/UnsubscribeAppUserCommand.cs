using System;

namespace BookActivity.Domain.Commands.AppUserCommands.UnsubscribeAppUser
{
    public sealed class UnsubscribeAppUserCommand : AppUserCommand
    {
        public Guid UserIdWhoUnsubscribed { get; set; }
        public Guid UnsubscribedUserId { get; set; }

        public UnsubscribeAppUserCommand(Guid unsubscribedUserId, Guid userIdWhoUnsubscribed)
        {
            UnsubscribedUserId = unsubscribedUserId;
            UserIdWhoUnsubscribed = userIdWhoUnsubscribed;
        }
    }
}
