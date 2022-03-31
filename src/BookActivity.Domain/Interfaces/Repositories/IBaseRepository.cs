using BookActivity.Domain.Models;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<T> GetByAsync(Expression<Func<T, bool>> condition);
        Task<IEnumerable<T>> GetByAsync(Expression<Func<T, bool>> condition, int skip, int take);

        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}