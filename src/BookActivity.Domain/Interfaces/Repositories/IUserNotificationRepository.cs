using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Models;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IUserNotificationRepository : IRepository<UserNotification>
    {
        Task<IEnumerable<UserNotification>> GetBySpecAsync(ISpecification<UserNotification> specification);
    }
}
