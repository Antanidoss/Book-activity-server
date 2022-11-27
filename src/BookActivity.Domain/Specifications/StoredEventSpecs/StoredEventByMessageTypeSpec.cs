using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Core.Events;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.StoredEventSpecs
{
    public sealed class StoredEventByMessageTypeSpec : ISpecification<StoredEvent>
    {
        private readonly string _messageType;

        public StoredEventByMessageTypeSpec(string messageType)
        {
            _messageType = messageType;
        }

        public Expression<Func<StoredEvent, bool>> ToExpression()
        {
            return e => e.MessageType == _messageType;
        }
    }
}
