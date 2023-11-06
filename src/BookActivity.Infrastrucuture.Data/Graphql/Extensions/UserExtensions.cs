using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Shared.Models;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookActivity.Infrastructure.Data.Graphql.Extensions
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

        public bool GetIsSubscription([Parent] AppUser user, [FromServices] BookActivityContext context, [FromServices] CurrentUser curUser)
        {
            if (user.Id == curUser.Id)
                return false;

            return context.Subscriptions.Any(s => s.SubscribedUserId == user.Id && s.UserIdWhoSubscribed == curUser.Id);
        }

        public int GetBookOpinionCount([Parent] AppUser user, [FromServices] BookActivityContext context)
        {
            return context.BookOpinions.Where(o => o.UserId == user.Id).Count();
        }

        public int GetActiveBookCount([Parent] AppUser user, [FromServices] BookActivityContext context)
        {
            return context.ActiveBooks.Where(o => o.UserId == user.Id).Count();
        }

        [BindMember(nameof(AppUser.AvatarImage))]
        public string GetAvatarDataBase64([Parent] AppUser user)
        {
            return Convert.ToBase64String(user.AvatarImage);
        }
    }
}
