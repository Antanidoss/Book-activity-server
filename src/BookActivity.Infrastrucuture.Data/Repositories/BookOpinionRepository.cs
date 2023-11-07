using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.EF;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class BookOpinionRepository : IBookOpinionRepository
    {
        private readonly BookActivityContext _db;

        private readonly DbSet<BookOpinion> _dbSet;

        public IUnitOfWork UnitOfWork => _db;

        public BookOpinionRepository(BookActivityContext context)
        {
            _db = context;
            _dbSet = _db.Set<BookOpinion>();
        }

        public void Add(BookOpinion bookOpinion)
        {
            CommonValidator.ThrowExceptionIfNull(bookOpinion);

            _dbSet.Add(bookOpinion);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
