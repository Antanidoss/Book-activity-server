using BookActivity.Domain.Core.Events;
using BookActivity.Domain.Interfaces.Repositories;
using NetDevPack.Messaging;
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

        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                string.Empty);

            _eventStoreRepository.Save(storedEvent);
        }
    }
}