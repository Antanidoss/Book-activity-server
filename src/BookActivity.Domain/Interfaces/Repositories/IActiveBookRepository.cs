using Antanidoss.Specification.Filters.Interfaces;
using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Models;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IActiveBookRepository : IRepository<ActiveBook>
    {
        ActiveBook GetByFilter(IQueryableSingleResultFilter<ActiveBook> filter);
        Task<IEnumerable<ActiveBook>> GetByFilterAsync(ActiveBookFilterModel filterModel, params Expression<Func<ActiveBook, object>>[] includes);
        Task<int> GetCountByFilterAsync(IQueryableMultipleResultFilter<ActiveBook> filter, int skip = 0);
        void Add(ActiveBook activeBook);
        void Update(ActiveBook activeBook);
        void Remove(ActiveBook activeBook);
    }
}