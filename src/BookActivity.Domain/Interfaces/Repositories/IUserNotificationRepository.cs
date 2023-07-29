using BookActivity.Domain.Filters;
using BookActivity.Domain.Models;
using NetDevPack.Data;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IUserNotificationRepository : IRepository<UserNotification>, IITransactionRepository
    {
        Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<UserNotification, TResult> filterModel);

        void Add(UserNotification notification);
    }
}
