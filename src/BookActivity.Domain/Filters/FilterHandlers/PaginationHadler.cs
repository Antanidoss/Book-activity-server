using System.Linq;
using System.Data.Entity;
using NetDevPack.Domain;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;

namespace BookActivity.Domain.Filters.Handlers
{
    public static class PaginationHadler
    {
        public static IQueryable<TEntity> ApplyPaginaton<TEntity>(this IQueryable<TEntity> entities, PaginationModel paginationModel, bool withDefaultOrder = true)
            where TEntity : Entity
        {
            return (withDefaultOrder ? entities.OrderBy(e => e.Id) : entities)
                .Skip(() => paginationModel.Skip.Value)
                .Take(() => paginationModel.Take.Value);
        }

        public static IQueryable<TEntity> ApplyPaginaton<TEntity>(this IQueryable<TEntity> entities, int skip, bool withDefaultOrder = true)
           where TEntity : Entity
        {
            return (withDefaultOrder ? entities.OrderBy(e => e.Id) : entities)
                .Skip(() => skip);
        }

        public static IQueryable<AppUser> ApplyPaginaton(this IQueryable<AppUser> entities, PaginationModel paginationModel, bool withDefaultOrder = true)
        {
            return (withDefaultOrder ? entities.OrderBy(e => e.Id) : entities)
                .Skip(() => paginationModel.Skip.Value)
                .Take(() => paginationModel.Take.Value);
        }

        public static IQueryable<AppUser> ApplyPaginaton(this IQueryable<AppUser> entities, int skip, bool withDefaultOrder = true)
        {
            return (withDefaultOrder ? entities.OrderBy(e => e.Id) : entities)
                .Skip(() => skip);
        }
    }
}
