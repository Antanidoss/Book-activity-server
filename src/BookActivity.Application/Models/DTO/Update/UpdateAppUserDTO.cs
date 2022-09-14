using Microsoft.AspNetCore.Http;
using System;

namespace BookActivity.Application.Models.DTO.Update
{
    public class UpdateAppUserDTO
    {
        public Guid AppUserId { get; set; }
        public IFormFile AvatarImage { get; set; }
    }
}
