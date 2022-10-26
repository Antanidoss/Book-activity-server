using System.Linq;
using System.Data.Entity;
using NetDevPack.Domain;

namespace BookActivity.Domain.Filters.Handlers
{
    public static class PaginationHadler
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
