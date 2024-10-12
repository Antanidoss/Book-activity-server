using BookActivity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace BookActivity.Domain.Interfaces
{
    public interface IDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<AppUser> Users { get; set; }
        DbSet<ActiveBook> ActiveBooks { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<BookOpinion> BookOpinions { get; set; }
        DbSet<BookNote> BookNotes { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<Subscriber> Subscribers { get; set; }
        DbSet<Subscription> Subscriptions { get; set; }
        DbSet<BookNoteLike> BookNoteLikes { get; set; }
        DbSet<BookNoteDislike> BookNoteDislikes { get; set; }
        DbSet<BookOpinionLike> BookOpinionLikes { get; set; }
        DbSet<BookOpinionDislike> BookOpinionDislikes { get; set; }
        DatabaseFacade Database { get; }
        Task SaveNotificationChangesAsync(CancellationToken cancellationToken = default);
        Task SaveCommandChangesAsync(string savePoint = null, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
