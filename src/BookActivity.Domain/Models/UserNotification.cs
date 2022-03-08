using System;

namespace BookActivity.Domain.Models
{
    public class UserNotification : BaseEntity
    {
        /// <summary>
        /// Notification Description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Relation of user notification with the user
        /// </summary>
        public AppUser User { get; private set; }
        public Guid UserId { get; private set; }

        protected UserNotification() : base() { }

        public UserNotification(string description, Guid userId, bool isPublic) : base(isPublic)
        {
            Description = description;
            UserId = userId;
        }
    }
}
