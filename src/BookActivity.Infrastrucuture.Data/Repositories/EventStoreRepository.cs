using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<IList<TEvent>> GetBySpecificationAsync<TEvent>(string eventType, Specification<TEvent> specification) where TEvent : Domain.Core.Events.Event
        {
            var events = _db.GetCollection<TEvent>(eventType);

            var a = events
                .AsQueryable()
                .ToList();

            return events
                .AsQueryable()
                .Where(specification)
                .ToList();
        }

        public async Task SaveAsync<TEvent>(TEvent @event) where TEvent : Domain.Core.Events.Event
        {
            var collection = _db.GetCollection<TEvent>(@event.MessageType);

            await collection.InsertOneAsync(@event);
        }
    }
}
