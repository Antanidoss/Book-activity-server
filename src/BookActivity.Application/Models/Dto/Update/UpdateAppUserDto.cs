using Microsoft.AspNetCore.Http;
using System;

namespace BookActivity.Application.Models.Dto.Update
{
    public class UpdateAppUserDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public IFormFile AvatarImage { get; set; }
    }
}
