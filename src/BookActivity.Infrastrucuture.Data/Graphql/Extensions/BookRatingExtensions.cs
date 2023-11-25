using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc;
using BookActivity.Shared.Models;

namespace BookActivity.Infrastructure.Data.Graphql.Extensions
{
    [ExtendObjectType(typeof(BookRating))]
    public class BookRatingExtensions
    {
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<BookOpinion> GetBookOpinions([Service] BookActivityContext context, [Parent] BookRating bookRating)
        {
            return context.BookOpinions.Where(b => b.BookRatingId == bookRating.Id);
        }

        public float GetAverageRating([Service] BookActivityContext context, [Parent] BookRating bookRating)
        {
            return context.BookRatings
                .Include(r => r.BookOpinions)
                .Where(r => r.Id == bookRating.Id)
                .Select(r => r.GetAverageRating())
                .First();
        }

        public bool GetHasOpinion([Service] BookActivityContext context, [Parent] BookRating bookRating, [FromServices] CurrentUser currentUser, Guid? userId)
        {
            userId = userId ?? currentUser.Id;

            return context.BookRatings
                .Include(r => r.BookOpinions)
                .Where(r => r.Id == bookRating.Id)
                .Select(r => r.BookOpinions.Any(o => o.UserId == userId.Value))
                .First();
        }
    }
}
