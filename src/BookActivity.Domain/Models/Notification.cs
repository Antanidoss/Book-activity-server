using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public class Notification : BaseEntity
    {
        public string Description { get; private set; }
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
