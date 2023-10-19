using BookActivity.Domain.Core.Events;
using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Core
{
    public class BaseEntity : IAggregateRoot
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Time of creation entity
        /// </summary>
        public DateTime TimeOfCreation { get; set; }

        /// <summary>
        /// Time of update entity
        /// </summary>
        public DateTime TimeOfUpdate { get; set; }

        public IReadOnlyCollection<Event> DomainEvents => _domainEvents?.AsReadOnly();
        private List<Event> _domainEvents;

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public void AddDomainEvent(Event domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<Event>();
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(Event domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
