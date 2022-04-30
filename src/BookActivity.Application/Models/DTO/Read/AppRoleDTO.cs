using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Read
{
    public sealed class AppRoleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<AppUserDTO> User { get; set; }
        public AppRoleDTO() { }
        public AppRoleDTO(Guid id, string name, ICollection<AppUserDTO> user)
        {
            Id = id;
            Name = name;
            User = user;
        }
    }
}