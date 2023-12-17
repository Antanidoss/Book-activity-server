using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public class Notification : BaseEntity
    {
        /// <summary>
        /// Notification Description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Relation of user notification with the user
        /// </summary>
        public AppUser ToUser { get; private set; }
        public Guid ToUserId { get; private set; }

        protected Notification() : base() { }

        public Notification(string description, Guid toUserId)
        {
            Description = description;
            ToUserId = toUserId;
        }
    }
}
