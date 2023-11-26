using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using BookActivity.Shared.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookActivity.Infrastructure.Data.Graphql.Extensions
{
    [ExtendObjectType(typeof(Book))]
    public sealed class BookExtensions
    {
        public bool GetIsActiveBook([Parent] Book book, [FromServices] BookActivityContext context, [FromServices] IServiceProvider serviceProvider)
        {
            var user = serviceProvider.GetService(typeof(CurrentUser)) as CurrentUser;
            if (user == null)
                return false;

            return context.ActiveBooks.Any(a => a.UserId == user.Id && a.BookId == book.Id);
        }

        [BindMember(nameof(Book.ImageData))]
        public string GetImageDataBase64([Parent] Book book)
        {
            return Convert.ToBase64String(book.ImageData);
        }

        public float GetAverageRating([Service] BookActivityContext context, [Parent] Book book)
        {
            return context.Books
                .Include(r => r.BookOpinions)
                .Where(b => b.Id == book.Id)
                .Select(b => b.GetAverageRating())
                .First();
        }

        public bool GetHasOpinion([Service] BookActivityContext context, [Parent] Book book, [FromServices] IServiceProvider serviceProvider, Guid? userId)
        {
            userId = userId ?? serviceProvider.GetService<CurrentUser>()?.Id;

            return userId != null && context.Books
                .Include(b => b.BookOpinions)
                .Where(b => b.Id == book.Id)
                .Select(r => r.BookOpinions.Any(o => o.UserId == userId.Value))
                .First();
        }

        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<BookOpinion> GetBookOpinions([Service] BookActivityContext context, [Parent] Book book)
        {
            return context.BookOpinions.Where(b => b.BookId == book.Id);
        }
    }
}
