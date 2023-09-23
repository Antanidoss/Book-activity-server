using System;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class UserNotificationDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
