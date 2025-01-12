using BookActivity.Domain.Core;
using System;

namespace BookActivity.Domain.Models
{
    public sealed class BookCategory : BaseEntity
    {
        public Book Book { get; set; }
        public Guid BookId { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }

        public BookCategory(Category category)
        {
            Category = category;
        }

        public BookCategory(Guid categoryId)
        {
            CategoryId = categoryId;
        }

        //private, parameterless constructor used by EF Core
        private BookCategory() { }
    }
}