using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Read
{
    public sealed class AppUserDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public ICollection<AppUserDTO> FollowedUsers { get; set; }
        public IEnumerable<BookOpinionDTO> BookOpinions { get; set; }
        public IList<ResponseOpinionDTO> ResponseOpinions { get; set; }
        public ICollection<UserNotificationDTO> UserNotifications { get; set; }

        public AppUserDTO() { }
        public AppUserDTO(ICollection<AppUserDTO> followedUsers, IEnumerable<BookOpinionDTO> bookOpinions, IList<ResponseOpinionDTO> responseOpinions, ICollection<UserNotificationDTO> userNotifications)
        {
            FollowedUsers = followedUsers;
            BookOpinions = bookOpinions;
            ResponseOpinions = responseOpinions;
            UserNotifications = userNotifications;
        }
    }
}