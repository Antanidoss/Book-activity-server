using System;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class SelectedAppUser
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public byte[] AvatarImage { get; set; }
    }
}
