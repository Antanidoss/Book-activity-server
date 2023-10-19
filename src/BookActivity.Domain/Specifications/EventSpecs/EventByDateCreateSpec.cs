using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Core.Events;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.EventSpecs
{
    public sealed class EventByDateCreateSpec<TEvent> : Specification<TEvent> where TEvent : Event
    {
        private readonly DateTime _dateCreated;

        public EventByDateCreateSpec(DateTime dateCreated)
        {
            _dateCreated = dateCreated;
        }

        public override Expression<Func<TEvent, bool>> ToExpression()
        {
            return e => e.Timestamp.Date == _dateCreated.Date;
        }
    }
}
