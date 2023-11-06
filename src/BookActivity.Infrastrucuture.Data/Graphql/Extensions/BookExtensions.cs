﻿using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Shared.Models;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Mvc;
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

        public int GetBookOpinionsCount([Parent] Book book, [FromServices] BookActivityContext context)
        {
            return context.BookOpinions.Where(b => b.BookRating.BookId == book.Id).Count();
        }

        [BindMember(nameof(Book.ImageData))]
        public string GetImageDataBase64([Parent] Book book)
        {
            return Convert.ToBase64String(book.ImageData);
        }
    }
}