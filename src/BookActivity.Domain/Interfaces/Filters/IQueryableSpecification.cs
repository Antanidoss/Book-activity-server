using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Domain.Interfaces.Filters
{
    public interface IQueryableSpecification<T> where T : BaseEntity
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }
}
