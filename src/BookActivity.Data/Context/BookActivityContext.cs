using BookActivity.Infrastructure.Data.Context.Configuration;
using BookActivity.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Domain;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace BookActivity.Infrastructure.Data.Context
{
    public class BookActivityContext : IdentityDbContext<AppUser, AppRole, Guid>, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public BookActivityContext(DbContextOptions<BookActivityContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<ActiveBook> ActiveBooks { get; set; }
        public DbSet<ResponseOpinion> ResponseOpinions { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookOpinion> BookOpinions { get; set; }
        public DbSet<BookNote> BookNotes { get; set; }

        public async Task<bool> Commit()
        {
            await _mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);
            var success = await SaveChangesAsync() > 0;

            return success;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfiguration(new BookOptionConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            DbSaveChanges();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            DbSaveChanges();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void DbSaveChanges()
        {
            var addedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is BaseEntity);

            foreach (var entry in addedEntities)
            {
                var timeOfCreate = entry.Property(nameof(BaseEntity.TimeOfCreation)).CurrentValue;

                if (timeOfCreate == null || DateTime.Parse(timeOfCreate.ToString()).Year < 2021)
                {
                    entry.Property(nameof(BaseEntity.TimeOfCreation)).CurrentValue = DateTime.UtcNow;
                }
            }

            var updateEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Entity is BaseEntity);

            foreach (var entry in updateEntities)
            {
                var timeOfUpdate = entry.Property(nameof(BaseEntity.TimeOfUpdate)).CurrentValue;

                if (timeOfUpdate == null || DateTime.Parse(timeOfUpdate.ToString()).Year < 2021)
                {
                    entry.Property(nameof(BaseEntity.TimeOfUpdate)).CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }

    public static class MediatorExtension
    {
        /// <summary>
        /// Sends events to all handlers
        /// </summary>
        public static async Task PublishDomainEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities
                .ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}