using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly BookActivityContext _context;

        private readonly DbSet<UserNotification> _dbSet;

        public UserNotificationRepository(BookActivityContext context)
        {
            _context = context;
            _dbSet = context.UserNotifications;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<UserNotification>> GetBySpecAsync(ISpecification<UserNotification> specification)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(specification.ToExpression())
                .ToListAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
