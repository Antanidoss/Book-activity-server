using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal class BookNoteRepository : IBookNoteRepository
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
            _dbSet.Add(bookNote);
        }

        public void Remove(BookNote bookNote)
        {
            _dbSet.Remove(bookNote);
        }

        public void Update(BookNote bookNote)
        {
            _dbSet.Update(bookNote);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
