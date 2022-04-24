using System;

namespace BookActivity.Domain.Filters.Models
{
    public class BookFilterModel : BaseFilterModel
    {
        public Guid BookId { get; set; }

        public string Title { get; set; }

        protected BookFilterModel() { }
        public BookFilterModel(Guid bookId, string title ,int skip, int take) : base(skip, take)
        {
            BookId = bookId;
            Title = title;
        }
    }
}
