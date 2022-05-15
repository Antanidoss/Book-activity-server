using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Domain.Interfaces.Filters
{
    public interface IFilterHandler<TEntity, TFilterModel>
        where TEntity : BaseEntity
        where TFilterModel : BaseFilterModel
    {
        IQueryable<TEntity> Handle(TFilterModel filterModel, IQueryable<TEntity> entities);
    }
}
