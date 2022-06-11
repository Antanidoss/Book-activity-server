using BookActivity.Domain.Interfaces.Filters;
using LinqKit;
using NetDevPack.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookActivity.Domain.Filters
{
    public class OrIQueryableFilterSpec<TEntityType> : IQueryableFilterSpec<TEntityType>
        where TEntityType : IAggregateRoot
    {
        private readonly IQueryableFilterSpec<TEntityType> _first;

        private readonly IQueryableFilterSpec<TEntityType> _second;

        public OrIQueryableFilterSpec(IQueryableFilterSpec<TEntityType> first, IQueryableFilterSpec<TEntityType> second)
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
            if (_first == null) return _second.ToExpression();
            else if (_second == null) return _first.ToExpression();

            return _first.ToExpression().Or(_second.ToExpression());
        }
    }
}
