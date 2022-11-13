using NetDevPack.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Filters.Models
{
    public sealed class FilterModel<TEntity> where TEntity : IAggregateRoot
    {
        public readonly Func<IQueryable<TEntity>, IQueryable<TEntity>> FilterHandler;

        public readonly Expression<Func<TEntity, object>>[] Includes;
        public FilterModel(Func<IQueryable<TEntity>, IQueryable<TEntity>> filterHandler, params Expression<Func<TEntity, object>>[] includes)
        {
            FilterHandler = filterHandler;
            Includes = includes;
        }
    }
}
