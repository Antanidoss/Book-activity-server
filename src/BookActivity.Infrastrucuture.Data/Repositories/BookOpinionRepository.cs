using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Domain.Validations;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetDevPack.Data;

namespace BookActivity.Infrastructure.Data.Repositories
{
    internal sealed class BookOpinionRepository : BaseRepository, IBookOpinionRepository
    {
        private readonly DbSet<BookOpinion> _dbSet;

        public IUnitOfWork UnitOfWork => Context;

        public BookOpinionRepository(BookActivityContext context, ILogger logger) : base(context, logger)
        {
            _dbSet = Context.Set<BookOpinion>();
        }

        public void Add(BookOpinion bookOpinion)
        {
            CommonValidator.ThrowExceptionIfNull(bookOpinion);

            _dbSet.Add(bookOpinion);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
