using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class AppUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public byte[] AvatarImage { get; set; }
        public ICollection<AppUserDto> FollowedUsers { get; set; }
        public IEnumerable<BookOpinionDto> BookOpinions { get; set; }
        public ICollection<UserNotificationDto> UserNotifications { get; set; }

        public AppUserDto() { }
        public AppUserDto(ICollection<AppUserDto> followedUsers, IEnumerable<BookOpinionDto> bookOpinions, ICollection<UserNotificationDto> userNotifications)
        {
            FollowedUsers = followedUsers;
            BookOpinions = bookOpinions;
            UserNotifications = userNotifications;
        }
    }
}
