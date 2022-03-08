using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO
{
    public class AppRoleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<AppUserDTO> User { get; set; }
    }
}