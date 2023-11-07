using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<Book, TResult> filterModel);
        Task<Book> GetByFilterAsync(DbSingleResultFilterModel<Book> filterModel);
        Task<int> GetCountByFilterAsync(DbMultipleResultFilterModel<Book> filterModel);
        void Add(Book book);
        void Remove(Book book);
    }
}