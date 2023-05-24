using LinqKit;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Infrastructure.Data.Extensions
{
    internal static class IQueryableExtensions
    {
        public static IQueryable<TEntity> IncludeMultiple<TEntity>(this IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IAggregateRoot
        {
            includes.ForEach((i) => query = query.Include(i));

            return query;
        }
    }
}
