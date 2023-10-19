using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Core.Events;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.EventSpecs
{
    public sealed class EventByUserIdSpec<TEvent> : Specification<TEvent> where TEvent : Event
    {
        private readonly Guid _userId;

        public EventByUserIdSpec(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<TEvent, bool>> ToExpression()
        {
            return e => e.UserId == _userId;
        }
    }
}
