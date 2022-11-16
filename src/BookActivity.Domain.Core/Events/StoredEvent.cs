using System;

namespace BookActivity.Domain.Core.Events
{
    public class StoredEvent : Event
    {
        public Guid Id { get; private set; }

        public string Data { get; protected set; }

        protected StoredEvent() { }
        public StoredEvent(Event @event, string data)
        {
            AggregateId = @event.AggregateId;
            MessageType = @event.MessageType;
            UserId = @event.UserId;
            Data = data;
        }
    }
}