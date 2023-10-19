using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public sealed class UserNotification : BaseEntity
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

        private UserNotification() : base() { }

        public UserNotification(string description, Guid userId)
        {
            Description = description;
            UserId = userId;
        }
    }
}
