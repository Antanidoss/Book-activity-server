using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IUserNotificationRepository : IRepository<UserNotification>
    {
        Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<UserNotification, TResult> filterModel);

        ValueTask AddAsync(UserNotification notification);

        void RemoveRange(Specification<UserNotification> specification);
    }
}
