using Antanidoss.Specification.Filters.Interfaces;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using NetDevPack.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetByFilterAsync(AuthorFilterModel filterModel);
        Author GetByFilter(IQueryableSingleResultFilter<Author> filter);
        Task<int> GetCountByFilterAsync(IQueryableMultipleResultFilter<Author> filter, int skip = 0);
        void Add(Author entity);
        void Remove(Author entity);
        void Update(Author entity);
    }
}
