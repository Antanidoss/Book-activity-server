using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Filters;
using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Application.Implementation.FilterHandlers
{
    public sealed class BookAuthorFilterHandler : IFilterHandler<BookAuthor, BookAuthorFilterModel>
    {
        public IQueryable<BookAuthor> Handle(BookAuthorFilterModel authorFilterModel, IQueryable<BookAuthor> query)
        {
            if (query == null) return null;

            if (authorFilterModel.AuthorIds != null || authorFilterModel.AuthorIds.Value.Any())
                query = authorFilterModel.AuthorIds.FilterSpec.ApplyFilter(query, authorFilterModel.AuthorIds.Value);

            return query.Skip(authorFilterModel.Skip).Take(authorFilterModel.Take);
        }
    }
}
