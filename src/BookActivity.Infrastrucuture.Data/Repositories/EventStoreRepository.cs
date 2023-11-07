using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Core;
using BookActivity.Domain.Interfaces.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
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

        public async Task<IList<TEvent>> GetBySpecificationAsync<TEvent>(string eventType, Specification<TEvent> specification) where TEvent : Event
        {
            var events = _db.GetCollection<TEvent>(eventType);

            return events
                .AsQueryable()
                .Where(specification)
                .ToList();
        }

        public async Task SaveAsync<TEvent>(TEvent @event) where TEvent : Event
        {
            var collection = _db.GetCollection<TEvent>(@event.MessageType);

            await collection.InsertOneAsync(@event);
        }
    }
}
