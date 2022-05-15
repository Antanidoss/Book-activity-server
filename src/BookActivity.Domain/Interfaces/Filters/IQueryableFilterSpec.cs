using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Domain.Interfaces.Filters
{
    public interface IQueryableFilterSpec<TEntity, TFilterParam>
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> entities, TFilterParam filterParam);
    }
}
