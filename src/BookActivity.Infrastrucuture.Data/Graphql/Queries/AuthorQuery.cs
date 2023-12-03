using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using HotChocolate.Data;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;

namespace BookActivity.Infrastructure.Data.Graphql.Queries
{
    [ExtendObjectType("Query")]
    public class AuthorQuery
    {
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Author> GetAuthors([Service] BookActivityContext context)
        {
            return context.Authors;
        }
    }
}
