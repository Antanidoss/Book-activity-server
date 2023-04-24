using BookActivity.Domain.Models;
using HotChocolate;
using HotChocolate.Data;
using System.Linq;

namespace BookActivity.Infrastructure.Data.Context
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Book> GetSuperheroes([Service] BookActivityContext context) => context.Books;
    }
}
