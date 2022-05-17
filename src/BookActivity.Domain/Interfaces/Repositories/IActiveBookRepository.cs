using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IActiveBookRepository : IRepository<ActiveBook>
    {
        Task<IEnumerable<ActiveBook>> GetByFilterAsync(ActiveBookFilterModel filterModel);
        Task<int> GetCountByFilterAsync(ActiveBookFilterModel filterModel);

        void Add(ActiveBook activeBook);
        void Update(ActiveBook activeBook);
        void Remove(ActiveBook activeBook);
    }
}