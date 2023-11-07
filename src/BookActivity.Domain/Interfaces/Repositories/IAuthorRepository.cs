using BookActivity.Domain.Models;
using NetDevPack.Data;
using System.Threading.Tasks;
using BookActivity.Domain.Filters.Models;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<TResult> GetByFilterAsync<TResult>(DbMultipleResultFilterModel<Author, TResult> filterModel);
        Task<Author> GetByFilterAsync(DbSingleResultFilterModel<Author> filterModel);
        Task<int> GetCountByFilterAsync(DbMultipleResultFilterModel<Author> filterModel);
        void Add(Author entity);
        void Remove(Author entity);
        void Update(Author entity);
    }
}
