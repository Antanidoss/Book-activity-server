using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using BookActivity.Shared.Models;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate;
using System;
using System.Linq;

namespace BookActivity.Infrastructure.Data.Graphql.Queries
{
    [ExtendObjectType("Query")]
    public class BookNoteQuery
    {

        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<BookNote> GetBookNotes([Service] BookActivityContext context, [Service] CurrentUser curUser, Guid? userId)
        {
            userId = userId ?? curUser.Id;

            return context.BookNotes.Where(n => n.ActiveBook.UserId == userId);
        }
    }
}
