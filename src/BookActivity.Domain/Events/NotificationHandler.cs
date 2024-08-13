using BookActivity.Domain.Interfaces;
using System.Threading.Tasks;

namespace BookActivity.Domain.Events
{
    public abstract class NotificationHandler
    {
        protected IDbContext _dbContext;

        public NotificationHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected async Task Commit()
        {
            await _dbContext.SaveNotificationChangesAsync();
        }
    }
}
