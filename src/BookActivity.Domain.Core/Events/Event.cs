using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BookActivity.Domain.Core.Events
{
    [BsonIgnoreExtraElements]
    public class Event : NetDevPack.Messaging.Event
    {
        public Guid? UserId { get; private set; }

        public virtual WhenCallHandler WhenCallHandler { get; }

        public Event() { }

        public Event(Guid? userId) : base()
        {
            UserId = userId;
        }
    }

    public enum WhenCallHandler
    {
        BeforeOperation,
        AfterOperation,
    }
}
