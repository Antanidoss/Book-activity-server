using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.BookSpecs
{
    public sealed class BookByTitleContainsSpec : ISpecification<Book>
    {
        private readonly string _title;

        public BookByTitleContainsSpec(string title)
        {
            _title = title;
        }

        public Expression<Func<Book, bool>> ToExpression()
        {
            return b => b.Title.Contains(_title);
        }
    }
}
