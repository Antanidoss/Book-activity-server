using Antanidoss.Specification.Filters.Interfaces;
using BookActivity.Domain.Models;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class AuthorFilterModel : BaseFilterModel
    {
        public IQueryableMultipleResultFilter<Author> Filter { get; set; }

        public AuthorFilterModel(int skip = SkipDefault, int take = TakeDefault) : base(skip, take) { }
        public AuthorFilterModel(IQueryableMultipleResultFilter<Author> filter, int skip = SkipDefault, int take = TakeDefault) : base(skip, take)
        {
            Filter = filter;
        }
    }
}
