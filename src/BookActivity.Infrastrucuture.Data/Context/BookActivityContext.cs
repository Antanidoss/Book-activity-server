using BookActivity.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using BookActivity.Domain.Core.Events;
using BookActivity.Domain.Interfaces;
using BookActivity.Infrastructure.Data.Context.Configuration;

namespace BookActivity.Infrastructure.Data.Context
{
    public sealed class BookActivityContext : IdentityDbContext<AppUser, AppRole, Guid>, IUnitOfWork
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<ActiveBook> ActiveBooks { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookOpinion> BookOpinions { get; set; }
        public DbSet<BookNote> BookNotes { get; set; }
        public DbSet<BookRating> BookRatings { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        private readonly IMediatorHandler _mediatorHandler;

        public BookActivityContext(DbContextOptions<BookActivityContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Commit()
        {
            await _mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);
            var success = await SaveChangesAsync() > 0;

            return success;
        }

        public override int SaveChanges()
        {
            UpdateCreationAndUpdateTime();

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateCreationAndUpdateTime();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<NetDevPack.Messaging.Event>();
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriberConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        private void UpdateCreationAndUpdateTime()
        {
            var addedEntities = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is BaseEntity);

            foreach (var entry in addedEntities)
            {
                var timeOfCreate = entry.Property(nameof(BaseEntity.TimeOfCreation)).CurrentValue;

                if (timeOfCreate == null || DateTime.Parse(timeOfCreate.ToString()).Year < DateTime.UtcNow.Year)
                    entry.Property(nameof(BaseEntity.TimeOfCreation)).CurrentValue = DateTime.UtcNow;
            }

            var updateEntities = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is BaseEntity);

            foreach (var entry in updateEntities)
            {
                var timeOfUpdate = entry.Property(nameof(BaseEntity.TimeOfUpdate)).CurrentValue;

                if (timeOfUpdate == null || DateTime.Parse(timeOfUpdate.ToString()).Year < DateTime.UtcNow.Year)
                    entry.Property(nameof(BaseEntity.TimeOfUpdate)).CurrentValue = DateTime.UtcNow;
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

            domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents.Select(async (domainEvent) => {
                await mediator.PublishEvent(domainEvent as Event);
            });

            await Task.WhenAll(tasks);
        }
    }
}