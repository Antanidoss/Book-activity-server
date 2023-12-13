using BookActivity.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task AddAsync(Subscription subscription, CancellationToken cancellationToken = default);
        void Remove(Subscription subscription);
    }
}
