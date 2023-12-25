using BookActivity.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookActivity.Domain.Queries.AppUserQueries.GetCurrentUser
{
    public sealed class GetCurrentUserQuery : Query<CurrentUser>
    {
        public readonly Guid UserId;

        public GetCurrentUserQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
