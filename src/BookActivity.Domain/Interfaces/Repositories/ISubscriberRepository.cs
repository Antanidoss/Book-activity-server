using BookActivity.Domain.Models;
using NetDevPack.Data;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface ISubscriberRepository : IRepository<Subscriber>
    {
        void Add(Subscriber subscriber);
        void Remove(Subscriber subscriber);
    }
}
