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
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetByFilterAsync(Func<IQueryable<Book>, IQueryable<Book>> filterHandler, params Expression<Func<Book, object>>[] includes);
        Task<Book> GetBySpecAsync(ISpecification<Book> specification);
        Task<Book> GetBySpecAsync(ISpecification<Book> specification, params Expression<Func<Book, object>>[] includes);
        Task<IEnumerable<Book>> GetBySpecAsync(ISpecification<Book> specification, PaginationModel paginationModel, params Expression<Func<Book, object>>[] includes);
        Task<int> GetCountByFilterAsync(Func<IQueryable<Book>, IQueryable<Book>> filterHandler, int skip = 0);
        void Add(Book book);
        void Update(Book book);
        void Remove(Book book);
    }
}