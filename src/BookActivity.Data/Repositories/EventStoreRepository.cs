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
    public sealed class EventStoreRepository : IEventStoreRepository
    {
        private readonly BookActivityEventStoreContext DB;

        public EventStoreRepository(BookActivityEventStoreContext db)
        {
            DB = db;
        }

        public void Dispose()
        {
            DB.Dispose();
        }

        public async Task<IList<StoredEvent>> GetAllAsync(Guid aggregateId)
        {
            return await DB.StoredEvent.Where(e => e.AggregateId == aggregateId).ToListAsync();
        }

        public void Save(StoredEvent @event)
        {
            DB.StoredEvent.Add(@event);
            DB.SaveChanges();
        }
    }
}
