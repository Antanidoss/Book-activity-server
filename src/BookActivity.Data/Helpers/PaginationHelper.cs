using BookActivity.Domain.Models;
using System.Linq;
using System.Data.Entity;
using NetDevPack.Domain;

namespace BookActivity.Infrastructure.Data.Helpers
{
    internal static class PaginationHelper
    {
        public static IQueryable<TEntity> ApplyPaginaton<TEntity>(this IQueryable<TEntity> entities, int? skip, int? take)
            where TEntity : IAggregateRoot
        {
            return entities
                .Skip(() => skip.Value)
                .Take(() => take.Value);
        }

        public static IQueryable<TEntity> ApplyPaginaton<TEntity>(this IQueryable<TEntity> entities, int skip)
           where TEntity : IAggregateRoot
        {
            return entities.Skip(() => skip);
        }
    }
}
