using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Core.Events;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Specifications.StoredEventSpecs
{
    public sealed class StoredEventByDateCreate : Specification<StoredEvent>
    {
        private readonly DateTime _dateCreated;

        public StoredEventByDateCreate(DateTime dateCreated)
        {
            _dateCreated = dateCreated;
        }

        public override Expression<Func<StoredEvent, bool>> ToExpression()
        {
            return e => e.Timestamp.Date == _dateCreated.Date;
        }
    }
}
