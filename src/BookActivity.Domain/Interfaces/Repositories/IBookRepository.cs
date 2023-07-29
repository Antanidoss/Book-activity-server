using BookActivity.Domain.Filters;
using BookActivity.Domain.Models;
using NetDevPack.Data;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IBookRepository : IRepository<Book>, IITransactionRepository
    {
        Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<Book, TResult> filterModel);
        Task<Book> GetByFilterAsync(DbSingleResultFilterModel<Book> filterModel);
        Task<int> GetCountByFilterAsync(DbMultipleResultFilterModel<Book> filterModel);
        void Add(Book book);
        void Remove(Book book);
    }
}