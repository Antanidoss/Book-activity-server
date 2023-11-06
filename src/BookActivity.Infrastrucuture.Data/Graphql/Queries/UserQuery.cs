using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate;
using System;
using System.Linq;
using BookActivity.Domain.Specifications.AppUserSpecs;

namespace BookActivity.Infrastructure.Data.Graphql.Queries
{
    [ExtendObjectType("Query")]
    public class UserQuery
    {
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<AppUser> GetUsers([Service] BookActivityContext context)
        {
            return context.Users;
        }

        public AppUser GetUserById([Service] BookActivityContext context, Guid userId)
        {
            return context.Users.FirstOrDefault(new AppUserByIdSpec(userId));
        }
    }
}
