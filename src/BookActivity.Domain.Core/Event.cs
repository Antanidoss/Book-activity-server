using MediatR;
using System;

namespace BookActivity.Domain.Core
{
    public class Event : INotification
    {
        public Guid? UserId { get; private set; }
        public virtual WhenCallHandler WhenCallHandler { get; }
        public DateTime Timestamp { get; private set; }
        public string MessageType { get; private set; }
        public Guid AggregateId { get; private set; }

        public Event()
        {
            Timestamp = DateTime.UtcNow;
            MessageType = GetType().Name;
        }

        public Event(Guid? userId, Guid aggregateId)
        {
            UserId = userId;
            AggregateId = aggregateId;
            Timestamp = DateTime.UtcNow;
            MessageType = GetType().Name;
        }

        public Event(Guid? userId)
        {
            UserId = userId;
            Timestamp = DateTime.UtcNow;
            MessageType = GetType().Name;
        }
    }

    public enum WhenCallHandler
    {
        BeforeOperation,
        AfterOperation,
    }
}
