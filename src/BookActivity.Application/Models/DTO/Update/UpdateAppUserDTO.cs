using System;

namespace BookActivity.Application.Models.DTO.Update
{
    public class UpdateAppUserDTO
    {
        public Guid AppUserId { get; set; }
        public byte[] AvatarImage { get; set; }
    }
}
