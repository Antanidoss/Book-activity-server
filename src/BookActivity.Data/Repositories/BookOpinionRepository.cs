using BookActivity.Domain.Interfaces.Repositories;
using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _dbSet.Add(bookOpinion);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
