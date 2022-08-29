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
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetByFilterAsync(BookFilterModel filterModel, params Expression<Func<Book, object>>[] includes);
        Book GetByFilterAsync(IQueryableSingleResultFilter<Book> filter);
        Task<int> GetCountByFilterAsync(IQueryableMultipleResultFilter<Book> filter, int skip);
        void Add(Book book);
        void Update(Book book);
        void Remove(Book book);
    }
}