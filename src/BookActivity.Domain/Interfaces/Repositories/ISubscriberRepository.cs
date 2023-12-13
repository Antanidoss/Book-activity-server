using BookActivity.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface ISubscriberRepository : IRepository<Subscriber>
    {
        Task AddAsync(Subscriber subscriber, CancellationToken cancellationToken = default);
        void Remove(Subscriber subscriber);
    }
}
