using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Read
{
    public class AppUserDTO
    {
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