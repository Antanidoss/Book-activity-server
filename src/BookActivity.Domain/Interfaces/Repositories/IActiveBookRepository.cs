using Antanidoss.Specification.Interfaces;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IActiveBookRepository : IRepository<ActiveBook>
    {
        Task<TResult> GetByFilterAsync<TResult>(Func<IQueryable<ActiveBook>, Task<TResult>> filter, params Expression<Func<ActiveBook, object>>[] includes);
        Task<ActiveBook> GetBySpecAsync(ISpecification<ActiveBook> specification, params Expression<Func<ActiveBook, object>>[] includes);
        Task<IEnumerable<ActiveBook>> GetBySpecAsync(ISpecification<ActiveBook> specification, PaginationModel paginationModel, params Expression<Func<ActiveBook, object>>[] includes);
        Task<int> GetCountByFilterAsync(Func<IQueryable<ActiveBook>, IQueryable<ActiveBook>> filterHandler, int skip = PaginationModel.SkipDefault);
        void Add(ActiveBook activeBook);
        void Update(ActiveBook activeBook);
        void Remove(ActiveBook activeBook);
    }
}