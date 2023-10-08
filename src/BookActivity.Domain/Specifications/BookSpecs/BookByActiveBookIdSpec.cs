using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.BookSpecs
{
    public sealed class BookByActiveBookIdSpec : Specification<Book>
    {
        private readonly Guid _activeBookId;

        public BookByActiveBookIdSpec(Guid activeBookId)
        {
            _activeBookId = activeBookId;
        }

        public override Expression<Func<Book, bool>> ToExpression()
        {
            return b => b.ActiveBooks.Any(a => a.Id == _activeBookId);
        }
    }
}
