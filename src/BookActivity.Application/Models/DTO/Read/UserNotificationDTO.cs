using System;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class UserNotificationDto : BaseEntityDto
    {
        public string Description { get; set; }
        public AppUserDto User { get; set; }
        public UserNotificationDto() : base() { }
        public UserNotificationDto(
            Guid UserNotificationId,
            DateTime timeOfCreation,
            DateTime timeOfUpdate,
            bool isPublic,
            string description,
            AppUserDto user) : base(UserNotificationId, timeOfCreation, timeOfUpdate, isPublic)
        {
            Description = description;
            User = user;
        }
    }
}