using Antanidoss.Specification.Abstract;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookActivity.Domain.Filters.Models
{
    public class DbMultipleResultFilterModel<TEntity, TResult> : DbFilterModel<TEntity> where TEntity : class
    {
        public readonly Func<IQueryable<TEntity>, Task<TResult>> Filter;

        public readonly PaginationModel PaginationModel;

        public DbMultipleResultFilterModel(
            Func<IQueryable<TEntity>, Task<TResult>> filter,
            PaginationModel paginationModel = null,
            bool forUpdate = false,
            params Expression<Func<TEntity, object>>[] includes) : base(forUpdate, includes)
        {
            Filter = filter;
            PaginationModel = paginationModel;
        }

        public DbMultipleResultFilterModel(
            Func<IQueryable<TEntity>, TResult> filter,
            PaginationModel paginationModel = null,
            bool forUpdate = false,
            params Expression<Func<TEntity, object>>[] includes) : base(forUpdate, includes)
        {
            Filter = async query => filter(query);
            PaginationModel = paginationModel;
        }

        public DbMultipleResultFilterModel(
            Func<IQueryable<TEntity>, Task<TResult>> filter,
            params Expression<Func<TEntity, object>>[] includes) : base(forUpdate: false, includes)
        {
            Filter = filter;
        }

        public static implicit operator DbMultipleResultFilterModel<TEntity, TResult>(Func<IQueryable<TEntity>, Task<TResult>> filter)
        {
            return new DbMultipleResultFilterModel<TEntity, TResult>(filter);
        }

        public static implicit operator DbMultipleResultFilterModel<TEntity, TResult>(Func<IQueryable<TEntity>, TResult> filter)
        {
            return new DbMultipleResultFilterModel<TEntity, TResult>(filter);
        }
    }

    public sealed class DbMultipleResultFilterModel<TEntity> : DbMultipleResultFilterModel<TEntity, IQueryable<TEntity>> where TEntity : class
    {
        public DbMultipleResultFilterModel(
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> filter,
            PaginationModel paginationModel = null,
            bool forUpdate = false,
            params Expression<Func<TEntity, object>>[] includes) : base(filter, paginationModel, forUpdate, includes)
        {
        }

        public DbMultipleResultFilterModel(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> filter,
            PaginationModel paginationModel = null,
            bool forUpdate = false,
            params Expression<Func<TEntity, object>>[] includes) : base(async query => filter(query), paginationModel, forUpdate, includes)
        {
        }

        public DbMultipleResultFilterModel(
            Specification<TEntity> specification,
            PaginationModel paginationModel = null,
            bool forUpdate = false,
            params Expression<Func<TEntity, object>>[] includes) : base(async query => query.Where(specification), paginationModel, forUpdate, includes)
        {
        }

        public static implicit operator DbMultipleResultFilterModel<TEntity>(Specification<TEntity> specification)
        {
            return new DbMultipleResultFilterModel<TEntity>(specification);
        }

        public static implicit operator DbMultipleResultFilterModel<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> filter)
        {
            return new DbMultipleResultFilterModel<TEntity>(filter);
        }
    }
}
