using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Queries.AppUserQueries.GetUserProfileInfo
{
    public class AppUserProfileInfo
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public byte[] AvatarImage { get; set; }
        public int SubscriptionsCount { get; set; }
        public int SubscribersCount { get; set; }
        public IEnumerable<ActiveBookInfoForProfile> ActiveBooks { get; set; }
    }

    public class ActiveBookInfoForProfile
    {
        public Guid Id { get; set; }
        public int TotalNumberPages { get; set; }
        public int NumberPagesRead { get; set; }
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}
