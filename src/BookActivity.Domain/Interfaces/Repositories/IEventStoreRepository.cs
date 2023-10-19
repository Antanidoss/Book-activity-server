using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Core.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IEventStoreRepository
    {
        Task<IList<TEvent>> GetBySpecificationAsync<TEvent>(string eventType, Specification<TEvent> specification) where TEvent : Event;
        Task SaveAsync<TEvent>(TEvent @event) where TEvent : Event;
    }
}