using NetDevPack.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Interfaces.Filters
{
    public interface IQueryableFilterSpec<TEntity>
        where TEntity : IAggregateRoot
    {
        IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> entities);
        Expression<Func<TEntity, bool>> ToExpression();
    }
}
