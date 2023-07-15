using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Shared;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookActivity.Infrastructure.Data.Graphql
{
    [ExtendObjectType(typeof(Book))]
    public class BookExtensions
    {
        public bool GetIsActiveBook([Parent] Book book, [FromServices] BookActivityContext context, [FromServices] CurrentUser user)
        {
            return context.ActiveBooks.Any(a => a.UserId == user.Id && a.BookId == book.Id);
        }
    }
}
