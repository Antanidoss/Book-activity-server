using System;

namespace BookActivity.Application.Models.DTO.Read
{
    public sealed class UserNotificationDTO : BaseEntityDTO
    {
        public string Description { get; set; }
        public AppUserDTO User { get; set; }
        public UserNotificationDTO() : base() { }
        public UserNotificationDTO(
            Guid UserNotificationId,
            DateTime timeOfCreation,
            DateTime timeOfUpdate,
            bool isPublic,
            string description,
            AppUserDTO user) : base(UserNotificationId, timeOfCreation, timeOfUpdate, isPublic)
        {
            Description = description;
            User = user;
        }
    }
}