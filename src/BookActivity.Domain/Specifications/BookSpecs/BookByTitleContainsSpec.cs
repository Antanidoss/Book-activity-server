using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Models;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.BookSpecs
{
    public sealed class BookByTitleContainsSpec : Specification<Book>
    {
        private readonly string _title;

        public BookByTitleContainsSpec(string title)
        {
            _title = title.ToLower().Replace(" ", string.Empty);
        }

        public override Expression<Func<Book, bool>> ToExpression()
        {
            return b => b.Title.ToLower().Replace(" ", string.Empty).Contains(_title);
        }
    }
}
