namespace BookActivity.Application.Models.DTO
{
    public class UserNotificationDTO : BaseEntityDTO
    {
        public string Description { get; set; }
        public AppUserDTO User { get; set; }
    }
}