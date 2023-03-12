using Antanidoss.Specification.Abstract;
using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Filters
{
    public sealed class DbSingleResultFilterModel<TEntity> : DbFilterModel<TEntity> where TEntity : class
    {
        public readonly Specification<TEntity> Specification;

        public DbSingleResultFilterModel(
            Specification<TEntity> specification,
            bool forUpdate = false,
            params Expression<Func<TEntity, object>>[] includes) : base(forUpdate, includes)
        {
            Specification = specification;
        }
    }
}
