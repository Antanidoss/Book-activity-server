using Antanidoss.Specification.Filters.Interfaces;
using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class BookRatingRepositiory : IBookRatingRepository
    {
        private readonly BookActivityContext _db;

        private readonly DbSet<BookRating> _dbSet;

        public IUnitOfWork UnitOfWork => _db;

        public BookRatingRepositiory(BookActivityContext context)
        {
            _db = context;
            _dbSet = _db.Set<BookRating>();
        }

        public BookRating GetByFilterAsync(IQueryableSingleResultFilter<BookRating> filter)
        {
            return filter.ApplyFilter(_dbSet.AsNoTracking());
        }

        public void Update(BookRating bookRating)
        {
            _dbSet.Update(bookRating);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
