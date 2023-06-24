using System.Linq;
using System.Data.Entity;
using NetDevPack.Domain;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;

namespace BookActivity.Domain.Filters.Handlers
{
    public static class PaginationHadler
    {
        public static IQueryable<TEntity> ApplyPaginaton<TEntity>(this IQueryable<TEntity> query, PaginationModel paginationModel, bool withDefaultOrder = true)
            where TEntity : Entity
        {
            if (paginationModel == null)
                return ApplyPaginaton(query, skip: 0, withDefaultOrder);

            return (withDefaultOrder ? query.OrderBy(e => e.Id) : query)
                .Skip(() => paginationModel.Skip.Value)
                .Take(() => paginationModel.Take.Value);
        }

        public static IQueryable<TEntity> ApplyPaginaton<TEntity>(this IQueryable<TEntity> query, int skip, bool withDefaultOrder = true)
           where TEntity : Entity
        {
            return (withDefaultOrder ? query.OrderBy(e => e.Id) : query)
                .Skip(() => skip);
        }

        public static IQueryable<AppUser> ApplyPaginaton(this IQueryable<AppUser> query, PaginationModel paginationModel, bool withDefaultOrder = true)
        {
            if (paginationModel == null)
                return ApplyPaginaton(query, skip: 0, withDefaultOrder);

            return (withDefaultOrder ? query.OrderBy(e => e.Id) : query)
                .Skip(() => paginationModel.Skip.Value)
                .Take(() => paginationModel.Take.Value);
        }

        public static IQueryable<AppUser> ApplyPaginaton(this IQueryable<AppUser> query, int skip, bool withDefaultOrder = true)
        {
            return (withDefaultOrder ? query.OrderBy(e => e.Id) : query)
                .Skip(() => skip);
        }
    }
}
