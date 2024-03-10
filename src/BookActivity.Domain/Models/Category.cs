using BookActivity.Domain.Core;
using System.Collections.Generic;

namespace BookActivity.Domain.Models
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }

        public Category() : base() { }
        public Category(string title) : base()
        {
            Title = title;
            BookCategories = new List<BookCategory>();
        }
        public Category(string title, params BookCategory[] bookCategories)
        {
            Title = title;
            BookCategories = bookCategories;
        }
    }
}
