using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Core.Events;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.StoredEventSpecs
{
    public sealed class StoredEventByUserIdSpec : Specification<StoredEvent>
    {
        private readonly Guid _userId;

        public StoredEventByUserIdSpec(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<StoredEvent, bool>> ToExpression()
        {
            return e => e.UserId == _userId;
        }
    }
}
