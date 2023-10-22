using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Core;
using MongoDB.Driver.Linq;
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
            return e => e.Timestamp.Truncate(DateTimeUnit.Year) == _dateCreated.Date.Truncate(DateTimeUnit.Year);
        }
    }
}
