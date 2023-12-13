using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using System.Threading.Tasks;
using System.Threading;

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

        public async Task AddAsync(Subscription subscription, CancellationToken cancellationToken = default)
        {
            await _db.Subscriptions.AddAsync(subscription, cancellationToken);
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
