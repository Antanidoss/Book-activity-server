using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Core.Events;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories.EventSourcing
{
    internal sealed class EventStoreRepository : IEventStoreRepository
    {
        private readonly BookActivityEventStoreContext _dB;

        public EventStoreRepository(BookActivityEventStoreContext db)
        {
            _dB = db;
        }

        public async Task<IList<StoredEvent>> GetAllAsync(Guid aggregateId)
        {
            return await _dB.StoredEvent
                .AsNoTracking()
                .Where(e => e.AggregateId == aggregateId)
                .ToListAsync();
        }

        public async Task<IList<StoredEvent>> GetBySpecificationAsync(Specification<StoredEvent> specification)
        {
            return await _dB.StoredEvent
                .AsNoTracking()
                .Where(specification)
                .ToListAsync();
        }

        public void Save(StoredEvent @event)
        {
            _dB.StoredEvent.Add(@event);
            _dB.SaveChanges();
        }

        public void Dispose()
        {
            _dB.Dispose();
        }
    }
}
