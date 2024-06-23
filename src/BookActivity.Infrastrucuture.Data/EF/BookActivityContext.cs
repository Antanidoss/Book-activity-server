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

namespace BookActivity.Infrastructure.Data.EF
{
    public sealed class BookActivityContext : IdentityDbContext<AppUser, AppRole, Guid>, IDbContext
    {
        private const string _postgresSQLProviderName = "PostgresSQL";
        private const string _mSSQLProviderName = "MSSQL";

        private readonly string[] _supportedDbProviders = new[] { "PostgresSQL", "MSSQL" };
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

        public async Task<bool> Commit()
        {
            var domainEntities = ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.State != EntityState.Unchanged && x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .Cast<Event>()
                .ToList();

            using CancellationTokenSource cancellationTokenSource = new();
            var cancellationToken = cancellationTokenSource.Token;

            await _mediatorHandler.PublishEventsAsync(domainEvents.Where(e => e.WhenCallHandler == WhenCallHandler.BeforeOperation), cancellationToken);

            var success = await SaveChangesAsync() > 0;

            if (success)
                await _mediatorHandler.PublishEventsAsync(domainEvents.Where(e => e.WhenCallHandler == WhenCallHandler.AfterOperation), cancellationToken);

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
    }
}