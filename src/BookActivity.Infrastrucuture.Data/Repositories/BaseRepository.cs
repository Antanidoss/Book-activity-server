using BookActivity.Infrastructure.Data.Context;
using Microsoft.Extensions.Logging;
using System;

namespace BookActivity.Infrastructure.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected BookActivityContext Context;

        protected ILogger Logger;

        public BaseRepository(BookActivityContext context, ILogger logger)
        {
            Context = context;
            Logger = logger;
        }

        public virtual bool InTransaction(Action action)
        {
            try
            {
                using var dbContextTransaction = Context.Database.BeginTransaction();

                action();

                dbContextTransaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Empty);

                return false;
            }
        }
    }
}
