using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository : IBaseRepository<BookAuthor, BookAuthorFilterModel>
    {
    }
}
