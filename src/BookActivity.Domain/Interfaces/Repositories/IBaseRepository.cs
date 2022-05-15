using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity, TFilterModel> : IRepository<TEntity> 
        where TEntity : BaseEntity
        where TFilterModel : BaseFilterModel
    {
        Task<IEnumerable<TEntity>> GetByFilterAsync(TFilterModel filterModel);
        Task<int> GetCountByFilterAsync(TFilterModel filterModel);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}