using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.BookSpecs
{
    public sealed class BookByIdSpec : ISpecification<Book>
    {
        private readonly Guid[] _bookIds;

        public BookByIdSpec(params Guid[] bookIds)
        {
            _bookIds = bookIds;
        }

        public Expression<Func<Book, bool>> ToExpression()
        {
            return b => _bookIds.Contains(b.Id);
        }
    }
}
