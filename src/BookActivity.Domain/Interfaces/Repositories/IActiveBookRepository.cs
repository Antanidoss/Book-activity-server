using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IActiveBookRepository : IRepository<ActiveBook>
    {
        Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<ActiveBook, TResult> filterModel);
        Task<ActiveBook> GetByFilterAsync(DbSingleResultFilterModel<ActiveBook> filterModel);
        Task<int> GetCountByFilterAsync(DbMultipleResultFilterModel<ActiveBook> filterModel);
        void Add(ActiveBook activeBook);
        void Remove(ActiveBook activeBook);
    }
}