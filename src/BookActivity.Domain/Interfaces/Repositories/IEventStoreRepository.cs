using BookActivity.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IEventStoreRepository
    {
        Task<IList<TEvent>> GetAllAsync<TEvent>(string messageType, Guid aggregateId) where TEvent : Event;
        Task<IList<TEvent>> GetBySpecificationAsync<TEvent>(string messageType, params (string Name, string Value)[] filter) where TEvent : Event;
        Task SaveAsync<TEvent>(TEvent @event) where TEvent : Event;
    }
}