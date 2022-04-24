using BookActivity.Domain.Models;
using System.Linq;

namespace BookActivity.Application.Interfaces
{
    public interface IQueryableSpecification<T> where T : BaseEntity
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }
}
