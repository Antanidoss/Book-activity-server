using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public sealed class Subscriber : BaseEntity
    {
        public AppUser UserWhoSubscribed { get; set; }
        public Guid UserIdWhoSubscribed { get; set; }
        public AppUser SubscribedUser { get; set; }
        public Guid SubscribedUserId { get; set; }

        public Subscriber(Guid userIdWhoSubscribed, Guid subscribedUserId)
        {
            UserIdWhoSubscribed = userIdWhoSubscribed;
            SubscribedUserId = subscribedUserId;
        }

        //private, parameterless constructor used by EF Core
        private Subscriber() { }
    }
}
