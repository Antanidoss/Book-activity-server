using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly BookActivityContext _db;

        public SubscriptionRepository(BookActivityContext context)
        {
            _db = context;
        }

        public IUnitOfWork UnitOfWork => _db;

        public void Add(Subscription subscription)
        {
            _db.Subscriptions.Add(subscription);
        }
        public void Remove(Subscription subscription)
        {
            _db.Subscriptions.Remove(subscription);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
