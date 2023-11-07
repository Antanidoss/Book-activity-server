using BookActivity.Domain.Models;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        void Add(Subscription subscription);
        void Remove(Subscription subscription);
    }
}
