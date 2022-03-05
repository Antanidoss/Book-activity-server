using BookActivity.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Save(StoredEvent @event);
        Task<IList<StoredEvent>> GetAllAsync(Guid aggregateId);
    }
}