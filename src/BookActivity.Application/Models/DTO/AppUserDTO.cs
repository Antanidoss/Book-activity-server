using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO
{
    public class AppUserDTO
    {
        public ICollection<AppUserDTO> FollowedUsers { get; set; }
        public IEnumerable<BookOpinionDTO> BookOpinions { get; set; }
        public IList<ResponseOpinionDTO> ResponseOpinions { get; set; }
        public ICollection<UserNotificationDTO> UserNotifications { get; set; }
    }
}