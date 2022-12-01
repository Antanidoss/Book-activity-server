using NetDevPack.Domain;
using System.Linq;

namespace BookActivity.Domain.Interfaces
{
    internal interface IFilterHandler<TEntity, TFilterModel> where TEntity : IAggregateRoot
    {
        IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, TFilterModel filterModel);
    }
}
