using Microsoft.AspNetCore.Http;
using System;

namespace BookActivity.Application.Models.Dto.Create
{
    public sealed class CreateAppUserDto : BaseCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile AvatarImage { get; set; }
    }
}
