using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookActivity.Domain.Models
{
    public class AppUser : IdentityUser<int>
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

        public AppUser() : base()
        {
            FollowedUsers = new List<AppUser>();
            BookOpinions = new List<BookOpinion>();
            FollowedUsers = new List<AppUser>();
        }
    }
}