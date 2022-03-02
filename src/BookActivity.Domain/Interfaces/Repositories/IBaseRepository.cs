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
        Task<ActiveBook> GetBy(Expression<Func<ActiveBook, bool>> condition);
        Task<IEnumerable<ActiveBook>> GetBy(Expression<Func<ActiveBook, bool>> condition, int skip, int take);

        void Add(ActiveBook entity);
        void Update(ActiveBook entity);
        void Remove(ActiveBook entity);
    }
}