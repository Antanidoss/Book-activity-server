using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.ActiveBookSpecs
{
    public sealed class ActiveBookByIdSpec : ISpecification<ActiveBook>
    {
        private readonly Guid[] _activeBookIds;

        public ActiveBookByIdSpec(params Guid[] activeBookIds)
        {
            _activeBookIds = activeBookIds;
        }

        public Expression<Func<ActiveBook, bool>> ToExpression()
        {
            return a => _activeBookIds.Contains(a.Id);
        }
    }
}
