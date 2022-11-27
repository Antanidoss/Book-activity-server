using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Core.Events;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.StoredEventSpecs
{
    public sealed class StoredEventByUserIdSpec : ISpecification<StoredEvent>
    {
        private readonly Guid _userId;

        public StoredEventByUserIdSpec(Guid userId)
        {
            _userId = userId;
        }

        public Expression<Func<StoredEvent, bool>> ToExpression()
        {
            return e => e.UserId == _userId;
        }
    }
}
