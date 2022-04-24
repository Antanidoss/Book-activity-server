using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Domain.Interfaces.Filters
{
    public interface IQueryableOrderBy<T> where T : BaseEntity
    {
        IOrderedQueryable<T> Apply(IQueryable<T> queryable);
    }
}
