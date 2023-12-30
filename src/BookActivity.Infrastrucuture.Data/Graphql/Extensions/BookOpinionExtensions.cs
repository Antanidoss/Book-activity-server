using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using HotChocolate.Data;
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
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<BookOpinionLike> GetLikes([Parent] BookOpinion bookOpinion, [Service(ServiceKind.Synchronized)] BookActivityContext context)
        {
            return context.BookOpinionLikes.Where(l => l.BookId == bookOpinion.BookId);
        }

        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<BookOpinionDislike> GetDislikes([Parent] BookOpinion bookOpinion, [Service(ServiceKind.Synchronized)] BookActivityContext context)
        {
            return context.BookOpinionDislikes.Where(l => l.BookId == bookOpinion.BookId);
        }

        public bool GetHasLike([Parent] BookOpinion bookOpinion, [Service] BookActivityContext context, [Service] IServiceProvider serviceProvider, Guid? userId)
        {
            userId = userId ?? serviceProvider.GetService<CurrentUser>()?.Id;

            return userId.HasValue && context.BookOpinions.Any(o => o.BookId == bookOpinion.BookId && o.Likes.Any(l => l.UserId == userId));
        }

        public bool GetHasDislike([Parent] BookOpinion bookOpinion, [Service] BookActivityContext context, [Service] IServiceProvider serviceProvider, Guid? userId)
        {
            userId = userId ?? serviceProvider.GetService<CurrentUser>()?.Id;

            return userId.HasValue && context.BookOpinions.Any(o => o.BookId == bookOpinion.BookId && o.Dislikes.Any(d => d.UserId == userId));
        }
    }
}
