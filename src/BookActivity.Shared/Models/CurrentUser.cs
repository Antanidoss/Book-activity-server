using System;

namespace BookActivity.Shared.Models
{
    public class CurrentUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public byte[] AvatarImage { get; set; }
        public string Token { get; set; }
    }
}
