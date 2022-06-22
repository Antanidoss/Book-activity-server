using BookActivity.Domain.Models;
using QueryableFilterSpecification.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Filters.Specifications.ActiveBookSpecs
{
    public sealed class ActiveBookByIdSpec : IQueryableFilterSpec<ActiveBook>
    {
        private readonly Guid[] _activeBookIds;

        public ActiveBookByIdSpec(params Guid[] activeBookIds)
        {
            _activeBookIds = activeBookIds;
        }

        public IQueryable<ActiveBook> ApplyFilter(IQueryable<ActiveBook> activeBooks)
        {
            return activeBooks.Where(ToExpression());
        }

        public Expression<Func<ActiveBook, bool>> ToExpression()
        {
            return a => _activeBookIds.Contains(a.Id);
        }
    }
}
