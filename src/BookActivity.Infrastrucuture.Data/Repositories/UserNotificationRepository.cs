using Antanidoss.Specification.Abstract;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.EF;
using BookActivity.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly BookActivityContext _db;

        private readonly DbSet<UserNotification> _dbSet;

        public UserNotificationRepository(BookActivityContext context)
        {
            _db = context;
            _dbSet = context.UserNotifications;
        }

        public IUnitOfWork UnitOfWork => _db;

        public async Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<UserNotification, TResult> filterModel)
        {
            CommonValidator.ThrowExceptionIfNull(filterModel);

            var query = _dbSet.IncludeMultiple(filterModel.Includes);

            if (!filterModel.ForUpdate)
                query = query.AsNoTracking();

            if (typeof(TResult) == typeof(IEnumerable<UserNotification>))
                query = query.ApplyPaginaton(filterModel.PaginationModel);

            return await filterModel.Filter(query);
        }

        public void Add(UserNotification notification)
        {
            _db.Add(notification);
        }

        public void RemoveRange(Specification<UserNotification> specification)
        {
            var userNotifications = _dbSet.Where(specification);

            _dbSet.RemoveRange(userNotifications);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
