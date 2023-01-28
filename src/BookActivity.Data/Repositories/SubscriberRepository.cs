using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using NetDevPack.Data;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class SubscriberRepository : ISubscriberRepository
    {
        private readonly BookActivityContext _context;

        public SubscriberRepository(BookActivityContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Subscriber subscriber)
        {
            _context.Subscribers.Add(subscriber);
        }
        public void Remove(Subscriber subscriber)
        {
            _context.Subscribers.Remove(subscriber);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
