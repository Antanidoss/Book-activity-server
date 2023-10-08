using System;
using System.Text.Json.Serialization;

namespace BookActivity.Domain.Core.Events
{
    public class Event : NetDevPack.Messaging.Event
    {
        [JsonInclude]
        public Guid? UserId { get; private set; }

        [JsonInclude]
        public new DateTime Timestamp { get; private set; }

        [JsonIgnore]
        public virtual WhenCallHandler WhenCallHandler { get; }

        [JsonInclude]
        public new Guid AggregateId { get; protected set; }

        public Event() { }

        public Event(Guid? userId)
        {
            UserId = userId;
            Timestamp = DateTime.Now;
        }
    }

    public enum WhenCallHandler
    {
        BeforeOperation,
        AfterOperation,
    }
}
