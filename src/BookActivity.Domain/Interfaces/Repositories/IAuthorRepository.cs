using BookActivity.Domain.Filters.FilterFacades;
using BookActivity.Domain.Models;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository : IBaseRepository<BookAuthor, BookAuthorFilter>
    {
    }
}
