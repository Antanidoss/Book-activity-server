using BookActivity.Domain.Core.Events;
using Microsoft.EntityFrameworkCore;

namespace BookActivity.Infrastructure.Data.Context
{
    public sealed class BookActivityEventStoreContext : DbContext
    {
        public BookActivityEventStoreContext(DbContextOptions<BookActivityEventStoreContext> options) : base(options) { }

        public DbSet<StoredEvent> StoredEvent { get; set; }
    }
}