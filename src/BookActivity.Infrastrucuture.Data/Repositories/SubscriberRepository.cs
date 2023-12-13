using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task AddAsync(Subscriber subscriber, CancellationToken cancellationToken = default)
        {
            await _db.Subscribers.AddAsync(subscriber, cancellationToken);
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
