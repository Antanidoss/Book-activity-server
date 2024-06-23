using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using BookActivity.Shared.Models;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookActivity.Infrastructure.Data.Graphql.Extensions
{
    [ExtendObjectType(typeof(AppUser))]
    public sealed class UserExtensions
    {
        public int GetSubscribersCount([Parent] AppUser user, [FromServices] BookActivityContext context)
        {
            return context.Subscribers.Count(s => s.SubscribedUserId == user.Id);
        }

        public int GetSubscriptionsCount([Parent] AppUser user, [FromServices] BookActivityContext context)
        {
            return context.Subscriptions.Count(s => s.UserIdWhoSubscribed == user.Id);
        }

        public bool GetIsSubscribed([Parent] AppUser user, [FromServices] BookActivityContext context, [FromServices] IServiceProvider serviceProvider)
        {
            var curUser = serviceProvider.GetService<CurrentUser>();

            if (curUser == null || user.Id == curUser.Id)
                return false;

            return context.Subscribers.Any(s => s.SubscribedUserId == user.Id && s.UserIdWhoSubscribed == curUser.Id);
        }

        public bool GetIsSubscription([Parent] AppUser user, [FromServices] BookActivityContext context, [FromServices] IServiceProvider serviceProvider)
        {
            var curUser = serviceProvider.GetService<CurrentUser>();

            if (curUser == null || user.Id == curUser.Id)
                return false;

            return context.Subscriptions.Any(s => s.SubscribedUserId == user.Id && s.UserIdWhoSubscribed == curUser.Id);
        }

        public int GetBookOpinionCount([Parent] AppUser user, [FromServices] BookActivityContext context)
        {
            return context.BookOpinions.Count(o => o.UserId == user.Id);
        }

        public int GetActiveBookCount([Parent] AppUser user, [FromServices] BookActivityContext context)
        {
            return context.ActiveBooks.Count(o => o.UserId == user.Id);
        }

        [BindMember(nameof(AppUser.AvatarImage))]
        public string GetAvatarDataBase64([Parent] AppUser user)
        {
            return Convert.ToBase64String(user.AvatarImage);
        }
    }
}
