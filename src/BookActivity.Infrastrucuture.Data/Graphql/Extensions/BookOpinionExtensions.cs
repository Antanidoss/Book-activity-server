using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using HotChocolate.Types;
using HotChocolate;
using System.Linq;
using System;
using BookActivity.Shared.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BookActivity.Infrastructure.Data.Graphql.Extensions
{
    [ExtendObjectType(typeof(BookOpinion))]
    public class BookOpinionExtensions
    {
        public int GetLikesCount([Parent] BookOpinion bookOpinion, [Service(ServiceKind.Synchronized)] BookActivityContext context)
        {
            return context.BookOpinionLikes.Count(l => l.BookId == bookOpinion.BookId && l.UserIdOpinion == bookOpinion.User.Id);
        }

        public int GetDislikesCount([Parent] BookOpinion bookOpinion, [Service(ServiceKind.Synchronized)] BookActivityContext context)
        {
            return context.BookOpinionDislikes.Count(d => d.BookId == bookOpinion.BookId && d.UserIdOpinion == bookOpinion.User.Id);
        }

        public bool GetHasLike([Parent] BookOpinion bookOpinion, [Service] BookActivityContext context, [Service] IServiceProvider serviceProvider, Guid? userId)
        {
            userId = userId ?? serviceProvider.GetService<CurrentUser>()?.Id;

            return userId.HasValue && context.BookOpinionLikes.Any(o => o.BookId == bookOpinion.BookId && o.UserIdWhoLike == userId && o.UserIdOpinion == bookOpinion.User.Id);
        }

        public bool GetHasDislike([Parent] BookOpinion bookOpinion, [Service] BookActivityContext context, [Service] IServiceProvider serviceProvider, Guid? userId)
        {
            userId = userId ?? serviceProvider.GetService<CurrentUser>()?.Id;

            return userId.HasValue && context.BookOpinionDislikes.Any(o => o.BookId == bookOpinion.BookId && o.UserIdWhoDislike == userId && o.UserIdOpinion == bookOpinion.User.Id);
        }
    }
}
