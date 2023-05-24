using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using NetDevPack.Data;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class SubscriberRepository : ISubscriberRepository
    {
        private readonly BookActivityContext _db;

        public SubscriberRepository(BookActivityContext context)
        {
            _db = context;
        }

        public IUnitOfWork UnitOfWork => _db;

        public void Add(Subscriber subscriber)
        {
            _db.Subscribers.Add(subscriber);
        }
        public void Remove(Subscriber subscriber)
        {
            _db.Subscribers.Remove(subscriber);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
