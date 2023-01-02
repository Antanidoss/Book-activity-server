using BookActivity.Domain.Models;
using NetDevPack.Data;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        void Add(Subscription subscription);
    }
}
