using System;

namespace BookActivity.Domain.Filters.Models
{
    public class BookAuthorFilterModel : BaseFilterModel
    {
        public readonly Guid[] AuthorIds;

        public BookAuthorFilterModel(Guid[] authorIds, int skip, int take) : base(skip, take)
        {
            AuthorIds = authorIds;
        }

        public BookAuthorFilterModel(Guid[] authorIds)
        {
            AuthorIds = authorIds;
        }
    }
}
