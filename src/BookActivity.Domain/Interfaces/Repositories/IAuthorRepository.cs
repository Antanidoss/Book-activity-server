using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository : IRepository<BookAuthor>
    {
        Task<IEnumerable<BookAuthor>> GetByFilterAsync(BookAuthorFilterModel filterModel);
        Task<int> GetCountByFilterAsync(BookAuthorFilterModel filterModel);

        void Add(BookAuthor entity);
        void Remove(BookAuthor entity);
        void Update(BookAuthor entity);
    }
}
