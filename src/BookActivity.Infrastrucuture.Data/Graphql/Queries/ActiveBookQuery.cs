using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate;
using System.Linq;
using BookActivity.Shared.Models;
using System;

namespace BookActivity.Infrastructure.Data.Graphql.Queries
{
    [ExtendObjectType("Query")]
    public class ActiveBookQuery
    {
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ActiveBook> GetActiveBooks([Service] BookActivityContext context, [Service] CurrentUser curUser, Guid? userId, bool withFullRead = true)
        {
            var query = userId.HasValue
                ? context.ActiveBooks.Where(a => a.UserId == userId)
                : context.ActiveBooks.Where(a => a.UserId == curUser.Id);

            if (!withFullRead)
                query = query.Where(a => a.TotalNumberPages != a.NumberPagesRead);

            return query;
        }
    }
}
