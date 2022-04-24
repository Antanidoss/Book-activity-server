using BookActivity.Domain.Filters.FilterFacades;
using BookActivity.Domain.Models;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IBookRepository : IBaseRepository<Book, BookFilter>
    {
    }
}