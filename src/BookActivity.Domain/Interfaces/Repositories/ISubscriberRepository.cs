using BookActivity.Domain.Models;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface ISubscriberRepository : IRepository<Subscriber>
    {
        void Add(Subscriber subscriber);
        void Remove(Subscriber subscriber);
    }
}
