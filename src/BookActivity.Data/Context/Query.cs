using BookActivity.Domain.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System.Linq;

namespace BookActivity.Infrastructure.Data.Context
{
    public class Query
    {
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ActiveBook> GetActiveBooks([Service] BookActivityContext context, bool withFullRead = true)
        {
            if (!withFullRead)
                return context.ActiveBooks.Where(a => a.TotalNumberPages != a.NumberPagesRead);

            return context.ActiveBooks;
        }
    }
}
