using System.Linq;
using System.Data.Entity;
using NetDevPack.Domain;
using BookActivity.Domain.Filters.Models;

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

        public static IQueryable<TEntity> ApplyPaginaton<TEntity>(this IQueryable<TEntity> entities, PaginationModel paginationModel)
            where TEntity : IAggregateRoot
        {
            return entities
                .Skip(() => paginationModel.Skip.Value)
                .Take(() => paginationModel.Take.Value);
        }

        public static IQueryable<TEntity> ApplyPaginaton<TEntity>(this IQueryable<TEntity> entities, int skip)
           where TEntity : IAggregateRoot
        {
            return entities.Skip(() => skip);
        }
    }
}
