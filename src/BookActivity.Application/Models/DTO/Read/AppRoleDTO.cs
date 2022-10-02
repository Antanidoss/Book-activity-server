using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class AppRoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<AppUserDto> User { get; set; }
        public AppRoleDto() { }
        public AppRoleDto(Guid id, string name, ICollection<AppUserDto> user)
        {
            Id = id;
            Name = name;
            User = user;
        }
    }
}