using System;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class AppUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public byte[] AvatarImage { get; set; }
    }
}
