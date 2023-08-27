using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Shared;
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

        public bool GetIsSubscribed([Parent] AppUser user, [FromServices] BookActivityContext context, [FromServices] CurrentUser curUser)
        {
            if (user.Id == curUser.Id)
                return false;

            return context.Subscriptions.Any(s => s.SubscribedUserId == user.Id && s.UserIdWhoSubscribed == curUser.Id);
        }
    }
}
