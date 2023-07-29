using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetDevPack.Data;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class BookNoteRepository : BaseRepository, IBookNoteRepository
    {
        private readonly DbSet<BookNote> _dbSet;

        public IUnitOfWork UnitOfWork => Context;

        public BookNoteRepository(BookActivityContext context, ILogger logger) : base(context, logger)
        {
            _dbSet = Context.Set<BookNote>();
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
            Context.Dispose();
        }
    }
}
