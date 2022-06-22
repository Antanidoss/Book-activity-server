using BookActivity.Domain.Models;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Filters.Specifications.BookSpecs
{
    public sealed class BookByBookIdSpec : IQueryableFilterSpec<Book>
    {
        private readonly Guid[] _bookIds;

        public BookByBookIdSpec(params Guid[] bookIds)
        {
            _bookIds = bookIds;
        }

        public IQueryable<Book> ApplyFilter(IQueryable<Book> books) 
        {
            return books.Where(ToExpression());
        }

        public Expression<Func<Book, bool>> ToExpression()
        {
            return b => _bookIds.Contains(b.Id);
        }
    }
}
