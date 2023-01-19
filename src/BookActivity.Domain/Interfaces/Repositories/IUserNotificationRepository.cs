using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Models;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IUserNotificationRepository : IRepository<UserNotification>
    {
        Task<IEnumerable<UserNotification>> GetBySpecAsync(Specification<UserNotification> specification);

        void Add(UserNotification notification);
    }
}
