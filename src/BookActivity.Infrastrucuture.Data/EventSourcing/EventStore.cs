using BookActivity.Domain.Core.Events;
using BookActivity.Domain.Interfaces.Repositories;
using Newtonsoft.Json;

namespace BookActivity.Infrastructure.Data.EventSourcing
{
    internal sealed class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public EventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void Save<T>(T @event) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(@event);

            var storedEvent = new StoredEvent(@event, serializedData);

            _eventStoreRepository.Save(storedEvent);
        }
    }
}