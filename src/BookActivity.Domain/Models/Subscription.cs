using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public sealed class Subscription : BaseEntity
    {
        public AppUser UserWhoSubscribed { get; set; }
        public Guid UserIdWhoSubscribed { get; set; }
        public AppUser SubscribedUser { get; set; }
        public Guid SubscribedUserId { get; set; }
    }
}
