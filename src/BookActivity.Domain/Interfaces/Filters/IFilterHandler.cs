using BookActivity.Domain.Filters.Models;
using NetDevPack.Domain;
using System.Linq;

namespace BookActivity.Domain.Interfaces.Filters
{
    public interface IFilterHandler<TEntity, TFilterModel>
        where TEntity : IAggregateRoot
        where TFilterModel : BaseFilterModel
    {
        IQueryable<TEntity> Handle(TFilterModel filterModel, IQueryable<TEntity> entities);
    }
}
