using BookActivity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces
{
    public interface IDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<ActiveBook> ActiveBooks { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookOpinion> BookOpinions { get; set; }
        public DbSet<BookNote> BookNotes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<BookNoteLike> BookNoteLikes { get; set; }
        public DbSet<BookNoteDislike> BookNoteDislikes { get; set; }
        public DbSet<BookOpinionLike> BookOpinionLikes { get; set; }
        public DbSet<BookOpinionDislike> BookOpinionDislikes { get; set; }
        Task<bool> Commit();
    }
}
