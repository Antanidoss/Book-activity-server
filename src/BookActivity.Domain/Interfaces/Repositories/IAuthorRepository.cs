using Antanidoss.Specification.Filters.Interfaces;
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
        BookAuthor GetByFilter(IQueryableSingleResultFilter<BookAuthor> filter);
        Task<int> GetCountByFilterAsync(IQueryableMultipleResultFilter<BookAuthor> filter, int skip = 0);
        void Add(BookAuthor entity);
        void Remove(BookAuthor entity);
        void Update(BookAuthor entity);
    }
}
