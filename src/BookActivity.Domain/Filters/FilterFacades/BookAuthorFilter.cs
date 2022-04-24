using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Filters.Specifications.Author;
using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Domain.Filters.FilterFacades
{
    public class BookAuthorFilter : BaseFilter<BookAuthor>
    {
        private readonly BookAuthorFilterModel _authorFilterModel;
        public BookAuthorFilter(BookAuthorFilterModel authorFilterModel) : base(authorFilterModel)
        {
            _authorFilterModel = authorFilterModel;
        }

        public override IQueryable<BookAuthor> ApplyFilter(IQueryable<BookAuthor> query)
        {
            if (query == null) return null;

            if (_authorFilterModel.AuthorIds != null || _authorFilterModel.AuthorIds.Any())
            {
                foreach(var authorId in _authorFilterModel.AuthorIds)
                {
                    BookAuthorByIdSpec authorByIdSpec = new(authorId);
                    query = authorByIdSpec.Apply(query);
                }
            }

            return base.ApplyFilter(query);
        }
    }
}
