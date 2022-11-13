using System.Linq;
using System.Data.Entity;
using NetDevPack.Domain;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;

namespace BookActivity.Domain.Filters.Handlers
{
    public static class PaginationHadler
    {
        public static IQueryable<TEntity> ApplyPaginaton<TEntity>(this IQueryable<TEntity> entities, int? skip, int? take)
            where TEntity : Entity
        {
            return entities
                .OrderBy(e => e.Id)
                .Skip(() => skip.Value)
                .Take(() => take.Value);
        }

        public static IQueryable<TEntity> ApplyPaginaton<TEntity>(this IQueryable<TEntity> entities, PaginationModel paginationModel)
            where TEntity : Entity
        {
            return entities
                .OrderBy(e => e.Id)
                .Skip(() => paginationModel.Skip.Value)
                .Take(() => paginationModel.Take.Value);
        }

        public static IQueryable<TEntity> ApplyPaginaton<TEntity>(this IQueryable<TEntity> entities, int skip)
           where TEntity : Entity
        {
            return entities
                .OrderBy(e => e.Id)
                .Skip(() => skip);
        }

        public static IQueryable<AppUser> ApplyPaginaton(this IQueryable<AppUser> entities, int? skip, int? take)
        {
            return entities
                .OrderBy(e => e.Id)
                .Skip(() => skip.Value)
                .Take(() => take.Value);
        }

        public static IQueryable<AppUser> ApplyPaginaton(this IQueryable<AppUser> entities, PaginationModel paginationModel)
        {
            return entities
                .OrderBy(e => e.Id)
                .Skip(() => paginationModel.Skip.Value)
                .Take(() => paginationModel.Take.Value);
        }

        public static IQueryable<AppUser> ApplyPaginaton(this IQueryable<AppUser> entities, int skip)
        {
            return entities
                .OrderBy(e => e.Id)
                .Skip(() => skip);
        }
    }
}
