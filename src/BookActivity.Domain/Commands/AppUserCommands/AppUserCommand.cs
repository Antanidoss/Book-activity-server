using System;

namespace BookActivity.Domain.Commands.AppUserCommands
{
    public class AppUserCommand : Command
    {
        public Guid AppUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] AvatarImage { get; set; }
        public string Description { get; set; }
    }
}
