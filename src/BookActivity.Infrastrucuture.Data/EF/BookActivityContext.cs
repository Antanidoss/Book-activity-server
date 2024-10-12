using BookActivity.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using BookActivity.Domain.Interfaces;
using BookActivity.Infrastructure.Data.EF.Configuration;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BookActivity.Domain.Core;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using BookActivity.Shared.Extensions;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookActivity.Infrastructure.Data.EF
{
    public sealed class BookActivityContext : IdentityDbContext<AppUser, AppRole, Guid>, IDbContext
    {
        private const string _postgresSQLProviderName = "PostgresSQL";
        private const string _mSSQLProviderName = "MSSQL";
        public DbSet<Book> Books { get; set; }
        public DbSet<ActiveBook> ActiveBooks { get; set; }
        public DbSet<Category> Categories { get; set; }
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

        private readonly IMediatorHandler _mediatorHandler;
        private readonly IConfiguration _configuration;

        public BookActivityContext(IMediatorHandler mediatorHandler, IConfiguration configuration) : base()
        {
            _mediatorHandler = mediatorHandler;
            _configuration = configuration;
        }

        public async Task SaveCommandChangesAsync(string savePoint = null, CancellationToken cancellationToken = default)
        {
            var domainEntities = ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.State != EntityState.Unchanged && x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            var beforeSaveEvents = domainEvents.Where(e => e.WhenCallHandler == WhenCallHandler.BeforeSave).ToArray();
            var afterSaveEvents = domainEvents.Where(e => e.WhenCallHandler == WhenCallHandler.AfterSave).ToArray();

            await SaveAsTransactionAsync(beforeSaveEvents, afterSaveEvents, savePoint, cancellationToken);
        }

        public override int SaveChanges()
        {
            UpdateCreationAndUpdateTime();

            return base.SaveChanges();
        }

        public async Task SaveNotificationChangesAsync(CancellationToken cancellationToken = default)
        {
            await SaveAsTransactionAsync(cancellationToken: cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateCreationAndUpdateTime();

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<ValidationResult>();
            builder.Ignore<Event>();

            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new SubscriberConfiguration());
            builder.ApplyConfiguration(new SubscriptionConfiguration());
            builder.ApplyConfiguration(new BookConfiguration());
            builder.ApplyConfiguration(new AuthorConfiguration());
            builder.ApplyConfiguration(new BookNoteConfiguration());
            builder.ApplyConfiguration(new BookOpinionConfiguration());
            builder.ApplyConfiguration(new NotificationConfiguration());
            builder.ApplyConfiguration(new BookOpinionLikeConfiguration());
            builder.ApplyConfiguration(new BookOpinionDislikeConfiguration());
            builder.ApplyConfiguration(new BookNoteLikeConfiguration());
            builder.ApplyConfiguration(new BookNoteDislikeConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());

            base.OnModelCreating(builder);
        }

        //TODO: Logs doesn't work
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var providerName = _configuration.GetDbProviderName();
            if (providerName == _postgresSQLProviderName)
                optionsBuilder.UseNpgsql(_configuration.GetPostgresSqlConnectionString());
            else if (providerName == _mSSQLProviderName)
                optionsBuilder.UseSqlServer(_configuration.GetMsSqlConnectionString());
            else
                throw new NotImplementedException($"SQL provider {providerName} not supported");

            optionsBuilder
                .EnableSensitiveDataLogging(_configuration.GetUseSqlLogs())
                .EnableDetailedErrors();
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

        private async Task SaveAsTransactionAsync(Event[] beforeSaveEvents = null, Event[] afterSaveEvents = null, string savePoint = null, CancellationToken cancellationToken = default)
        {
            var transaction = Database.CurrentTransaction;

            var publishBeforeSaveEventsAsync = async () =>
            {
                if (beforeSaveEvents != null)
                    await _mediatorHandler.PublishEventsAsync(beforeSaveEvents, cancellationToken);
            };

            var publishAfterSaveEventsAsync = async () =>
            {
                if (afterSaveEvents != null)
                    await _mediatorHandler.PublishEventsAsync(afterSaveEvents, cancellationToken);
            };

            if (transaction != null)
            {
                if (string.IsNullOrEmpty(savePoint))
                    await SaveChangesAsync(publishBeforeSaveEventsAsync, publishAfterSaveEventsAsync, cancellationToken);
                else
                    await SaveAsSavePointAsync(transaction, publishBeforeSaveEventsAsync, publishAfterSaveEventsAsync, savePoint, cancellationToken);
            }
            else
            {
                var strategy = Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    await using (transaction = await Database.BeginTransactionAsync(cancellationToken))
                    {
                        await publishBeforeSaveEventsAsync();

                        await SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);

                        await publishAfterSaveEventsAsync();
                    }
                });
            }
        }

        private async Task SaveAsSavePointAsync(
            IDbContextTransaction transaction,
            Func<Task> publishBeforeSaveEventsAsync,
            Func<Task> publishAfterSaveEventsAsync,
            string savePoint,
            CancellationToken cancellationToken)
        {
            if (!transaction.SupportsSavepoints)
                throw new NotSupportedException();

            await transaction.CreateSavepointAsync(savePoint, cancellationToken);

            try
            {
                await publishBeforeSaveEventsAsync();

                await SaveChangesAsync(cancellationToken);
                await transaction.ReleaseSavepointAsync(savePoint, cancellationToken);

                await publishAfterSaveEventsAsync();
            }
            catch
            {
                await transaction.RollbackToSavepointAsync(savePoint, cancellationToken);
                throw;
            }
        }
        private async Task SaveChangesAsync(
            Func<Task> publishBeforeSaveEventsAsync,
            Func<Task> publishAfterSaveEventsAsync,
            CancellationToken cancellationToken)
        {
            await publishBeforeSaveEventsAsync();
            await SaveChangesAsync(cancellationToken);
            await publishAfterSaveEventsAsync();
        }
    }
}