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
        private readonly BookActivityEventStoreContext _db;

        public EventStoreRepository(BookActivityEventStoreContext db)
        {
            _db = db;
        }

        public async Task<IList<StoredEvent>> GetAllAsync(Guid aggregateId)
        {
            return await _db.StoredEvent
                .AsNoTracking()
                .Where(e => e.AggregateId == aggregateId)
                .ToListAsync();
        }

        public async Task<IList<StoredEvent>> GetBySpecificationAsync(Specification<StoredEvent> specification)
        {
            return await _db.StoredEvent
                .AsNoTracking()
                .Where(specification)
                .ToListAsync();
        }

        public void Save(StoredEvent @event)
        {
            _db.StoredEvent.Add(@event);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
