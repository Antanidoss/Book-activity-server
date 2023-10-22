using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Core;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.EventSpecs
{
    public sealed class EventByMessageTypeSpec<TEvent> : Specification<TEvent> where TEvent : Event
    {
        private readonly string _messageType;

        public EventByMessageTypeSpec(string messageType)
        {
            _messageType = messageType;
        }

        public override Expression<Func<TEvent, bool>> ToExpression()
        {
            return e => e.MessageType == _messageType;
        }
    }
}
