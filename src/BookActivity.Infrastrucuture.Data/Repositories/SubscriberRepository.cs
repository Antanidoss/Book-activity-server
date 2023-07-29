using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.Extensions.Logging;
using NetDevPack.Data;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class SubscriberRepository : BaseRepository, ISubscriberRepository
    {
        public SubscriberRepository(BookActivityContext context, ILogger logger) : base(context, logger) { }

        public IUnitOfWork UnitOfWork => Context;

        public void Add(Subscriber subscriber)
        {
            Context.Subscribers.Add(subscriber);
        }

        public void Remove(Subscriber subscriber)
        {
            Context.Subscribers.Remove(subscriber);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
