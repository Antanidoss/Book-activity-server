using BookActivity.Domain.Interfaces.Filters;
using LinqKit;
using NetDevPack.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Filters
{
    public class AndIQueryableFilterSpec<TEntityType> : IQueryableFilterSpec<TEntityType>
        where TEntityType : IAggregateRoot
    {
        private readonly IQueryableFilterSpec<TEntityType> _first;

        private readonly IQueryableFilterSpec<TEntityType> _second;

        public AndIQueryableFilterSpec(IQueryableFilterSpec<TEntityType> first, IQueryableFilterSpec<TEntityType> second)
        {
            _first = first;
            _second = second;
        }

        public IQueryable<TEntityType> ApplyFilter(IQueryable<TEntityType> entities)
        {
            return entities.Where(ToExpression());
        }

        public Expression<Func<TEntityType, bool>> ToExpression()
        {
            return _first.ToExpression().And(_second.ToExpression());
        }
    }
}
