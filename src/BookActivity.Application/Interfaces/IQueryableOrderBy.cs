using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Application.Interfaces
{
    public interface IQueryableOrderBy<T> where T : BaseEntity
    {
        IOrderedQueryable<T> Apply(IQueryable<T> queryable);
    }
}
