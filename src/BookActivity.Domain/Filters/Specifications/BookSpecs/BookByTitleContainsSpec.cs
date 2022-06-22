using BookActivity.Domain.Models;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Filters.Specifications.BookSpecs
{
    public sealed class BookByTitleContainsSpec : IQueryableFilterSpec<Book>
    {
        private readonly string _title;

        public BookByTitleContainsSpec(string title)
        {
            _title = title;
        }

        public IQueryable<Book> ApplyFilter(IQueryable<Book> books)
        {
            return books.Where(ToExpression());
        }

        public Expression<Func<Book, bool>> ToExpression()
        {
            return b => b.Title.Contains(_title);
        }
    }
}
