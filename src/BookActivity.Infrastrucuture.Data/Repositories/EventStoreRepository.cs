using BookActivity.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories.EventSourcing
{
    internal sealed class EventStoreRepository : IEventStoreRepository
    {
        private readonly IMongoDatabase _db;

        public EventStoreRepository(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task<IList<TEvent>> GetAllAsync<TEvent>(string messageType, Guid aggregateId) where TEvent : Domain.Core.Events.Event
        {
            var events = _db.GetCollection<TEvent>(messageType);

            var filter = new BsonDocument { { "AggregateId", aggregateId.ToString() } };

            return await events.Find(filter).ToListAsync();
        }

        public async Task<IList<TEvent>> GetBySpecificationAsync<TEvent>(string messageType, params (string Name, string Value)[] filter) where TEvent : Domain.Core.Events.Event
        {
            var events = _db.GetCollection<TEvent>(messageType);

            var a = events.ToJson();
            
            BsonDocument bsomFilter = new()
            {
                new BsonElement("CountPagesRead", new BsonInt32(34))
            };


            //bsomFilter.AddRange(filter.Select(f => new BsonElement(f.Name, f.Value)));

            return await events.Find(bsomFilter).ToListAsync();
        }

        public async Task SaveAsync<TEvent>(TEvent @event) where TEvent : Domain.Core.Events.Event
        {
            var collection = _db.GetCollection<TEvent>(@event.MessageType);

            await collection.InsertOneAsync(@event);
        }
    }
}
