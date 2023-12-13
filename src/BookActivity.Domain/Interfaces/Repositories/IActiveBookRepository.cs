using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IActiveBookRepository : IRepository<ActiveBook>
    {
        Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<ActiveBook, TResult> filterModel, CancellationToken cancellationToken = default);
        Task<ActiveBook> GetByFilterAsync(DbSingleResultFilterModel<ActiveBook> filterModel, CancellationToken cancellationToken = default);
        Task<int> GetCountByFilterAsync(DbMultipleResultFilterModel<ActiveBook> filterModel, CancellationToken cancellationToken = default);
        Task AddAsync(ActiveBook activeBook, CancellationToken cancellationToken = default);
        void Remove(ActiveBook activeBook);
    }
}