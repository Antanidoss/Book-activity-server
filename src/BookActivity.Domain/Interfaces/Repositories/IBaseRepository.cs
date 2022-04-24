using BookActivity.Domain.Filters.FilterFacades;
using BookActivity.Domain.Models;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity, TFilter> : IRepository<TEntity> 
        where TEntity : BaseEntity
        where TFilter : BaseFilter<TEntity>
    {
        Task<IEnumerable<TEntity>> GetByFilterAsync(TFilter filter);
        Task<int> GetCountByFilterAsync(TFilter filter);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}