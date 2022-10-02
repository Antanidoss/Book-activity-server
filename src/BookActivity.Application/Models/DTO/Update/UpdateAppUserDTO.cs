using Microsoft.AspNetCore.Http;
using System;

namespace BookActivity.Application.Models.Dto.Update
{
    public class UpdateAppUserDto
    {
        public Guid AppUserId { get; set; }
        public string UserName { get; set; }
        public IFormFile AvatarImage { get; set; }
    }
}
