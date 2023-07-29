using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.Extensions.Logging;
using NetDevPack.Data;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class SubscriptionRepository : BaseRepository, ISubscriptionRepository
    {
        public SubscriptionRepository(BookActivityContext context, ILogger logger) : base(context, logger) { }

        public IUnitOfWork UnitOfWork => Context;

        public void Add(Subscription subscription)
        {
            Context.Subscriptions.Add(subscription);
        }
        public void Remove(Subscription subscription)
        {
            Context.Subscriptions.Remove(subscription);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
