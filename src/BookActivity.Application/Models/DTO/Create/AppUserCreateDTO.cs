using System;

namespace BookActivity.Application.Models.DTO.Create
{
    public sealed class AppUserCreateDTO : BaseCreateDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
