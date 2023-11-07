using System;
using System.Linq.Expressions;

namespace BookActivity.Domain.Filters.Models
{
    public abstract class DbFilterModel<TEntity> where TEntity : class
    {
        public readonly Expression<Func<TEntity, object>>[] Includes;

        public readonly bool ForUpdate;

        protected DbFilterModel(bool forUpdate, params Expression<Func<TEntity, object>>[] includes)
        {
            Includes = includes;
            ForUpdate = forUpdate;
        }
    }
}
