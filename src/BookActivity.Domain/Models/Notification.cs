﻿using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public class Notification : BaseEntity
    {
        public string Description { get; private set; }
        public AppUser ToUser { get; private set; }
        public Guid ToUserId { get; private set; }
        public AppUser FromUser { get; private set; }
        public Guid? FromUserId { get; private set; }

        public Notification(Guid notificationId)
        {
            Id = notificationId;
        }

        public Notification(string description, Guid toUserId, Guid? fromUserId = null)
        {
            Description = description;
            ToUserId = toUserId;
            FromUserId = fromUserId;
        }

        //private, parameterless constructor used by EF Core
        private Notification() { }
    }
}
