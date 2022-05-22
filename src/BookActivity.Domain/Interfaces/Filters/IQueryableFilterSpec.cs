using NetDevPack.Domain;
using System.Linq;

namespace BookActivity.Domain.Interfaces.Filters
{
    public interface IQueryableFilterSpec<TEntity, TFilterParam>
        where TEntity : IAggregateRoot
    {
        IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> entities, TFilterParam filterParam);
    }
}
