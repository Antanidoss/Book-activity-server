using BookActivity.Domain.Models;
using NetDevPack.Data;
using System.Threading.Tasks;
using Antanidoss.Specification.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using BookActivity.Domain.Filters.Models;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetByFilterAsync(Func<IQueryable<Author>, IQueryable<Author>> filterHandler);
        Task<Author> GetBySpecAsync(ISpecification<Author> specification);
        Task<IEnumerable<Author>> GetBySpecAsync(ISpecification<Author> specification, PaginationModel paginationModel);
        Task<int> GetCountBySpecAsync(ISpecification<Author> specification, int skip = 0);
        void Add(Author entity);
        void Remove(Author entity);
        void Update(Author entity);
    }
}
