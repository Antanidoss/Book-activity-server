using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookActivity.Infrastructure.Data.Graphql
{
    [ExtendObjectType(typeof(AppUser))]
    public sealed class UserExtensions
    {
        public int GetSubscribersCount([Parent] AppUser user, [FromServices] BookActivityContext context)
        {
            return context.Subscribers.Where(s => s.SubscribedUserId == user.Id).Count();
        }

        public int GetSubscriptionsCount([Parent] AppUser user, [FromServices] BookActivityContext context)
        {
            return context.Subscriptions.Where(s => s.UserIdWhoSubscribed == user.Id).Count();
        }
    }
}
