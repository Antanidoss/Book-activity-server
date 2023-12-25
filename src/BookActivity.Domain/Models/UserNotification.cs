using System;

namespace BookActivity.Domain.Models
{
    public sealed class UserNotification : Notification
    {
        public AppUser FromUser { get; private set; }
        public Guid FromUserId { get; private set; }

        public UserNotification() : base() { }

        public UserNotification(string description, Guid toUserId, Guid fromUserId) : base(description, toUserId)
        {
            FromUserId = fromUserId;
        }
    }
}
