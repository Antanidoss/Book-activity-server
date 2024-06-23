using Microsoft.AspNetCore.Http;

namespace BookActivity.Application.Models.Dto.Create
{
    public sealed class CreateUserDto : BaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile AvatarImage { get; set; }

        public override string Validate()
        {
            return string.Empty;
        }
    }
}
