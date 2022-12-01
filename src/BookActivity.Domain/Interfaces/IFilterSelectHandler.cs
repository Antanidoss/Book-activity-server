using NetDevPack.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces
{
    internal interface IFilterSelectHandler<TEntity, TResult, TFilterModel> where TEntity : IAggregateRoot
    {
        Task<TResult> Select(IQueryable<TEntity> query, TFilterModel filterModel);
    }
}
