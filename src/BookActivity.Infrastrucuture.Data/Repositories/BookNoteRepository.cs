using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.EF;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class BookNoteRepository : IBookNoteRepository
    {
        private readonly BookActivityContext _db;

        private readonly DbSet<BookNote> _dbSet;

        public IUnitOfWork UnitOfWork => _db;

        public BookNoteRepository(BookActivityContext context)
        {
            _db = context;
            _dbSet = _db.Set<BookNote>();
        }

        public void Add(BookNote bookNote)
        {
            CommonValidator.ThrowExceptionIfNull(bookNote);

            _dbSet.Add(bookNote);
        }

        public void Remove(BookNote bookNote)
        {
            CommonValidator.ThrowExceptionIfNull(bookNote);

            _dbSet.Remove(bookNote);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
