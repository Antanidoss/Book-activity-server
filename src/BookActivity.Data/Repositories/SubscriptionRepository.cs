using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using NetDevPack.Data;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly BookActivityContext _context;

        public SubscriptionRepository(BookActivityContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
