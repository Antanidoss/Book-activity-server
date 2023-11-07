using Microsoft.AspNetCore.Http;
using System;

namespace BookActivity.Application.Models.Dto.Update
{
    public class UpdateAppUserDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public IFormFile AvatarImage { get; set; }

        public override string Validate()
        {
            return string.Empty;
        }
    }
}
