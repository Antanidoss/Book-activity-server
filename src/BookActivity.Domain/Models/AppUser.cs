using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Relation of user with the subscription info
        /// </summary>
        public ICollection<AppUser> FollowedUsers { get; set; }

        /// <summary>
        /// Relation of user with the book opinios
        /// </summary>
        public IEnumerable<BookOpinion> BookOpinions { get; set; }

        /// <summary>
        /// Relation of user with the response opinios
        /// </summary>
        public IList<ResponseOpinion> ResponseOpinions { get; set; }

        /// <summary>
        /// Relation of user with the notifications
        /// </summary>
        public ICollection<UserNotification> UserNotifications { get; set; }

        public AppUser() : base()
        {
            FollowedUsers = new List<AppUser>();
            BookOpinions = new List<BookOpinion>();
            FollowedUsers = new List<AppUser>();
            UserNotifications = new List<UserNotification>();
        }
    }
}