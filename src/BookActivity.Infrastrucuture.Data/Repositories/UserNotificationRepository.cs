using BookActivity.Domain.Filters;
using BookActivity.Domain.Filters.Handlers;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class UserNotificationRepository : BaseRepository, IUserNotificationRepository
    {
        private readonly DbSet<UserNotification> _dbSet;

        public UserNotificationRepository(BookActivityContext context, ILogger logger) : base(context, logger)
        {
            _dbSet = context.UserNotifications;
        }

        public IUnitOfWork UnitOfWork => Context;

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
            Context.Add(notification);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
