using System;

namespace BookActivity.Domain.Queries.AppUserQueries.GetUserProfileInfo
{
    public sealed class GetUserProfileInfoQuery : Query<AppUserProfileInfo>
    {
        public Guid UserId { get; set; }
    }
}
