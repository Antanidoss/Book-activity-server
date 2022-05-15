using NetDevPack.Messaging;
using System;

namespace BookActivity.Domain.Commands.AppUserCommands
{
    public class AppUserCommand : Command
    {
        public Guid AppUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
