using BookActivity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookActivity.Infrastructure.Data.EF.Configuration
{
    internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasMany(u => u.Subscriptions)
                .WithOne(s => s.UserWhoSubscribed)
                .HasForeignKey(s => s.UserIdWhoSubscribed)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(u => u.Subscribers)
                .WithOne(s => s.SubscribedUser)
                .HasForeignKey(s => s.SubscribedUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(u => u.BookNoteLikes)
                .WithOne(l => l.UserWhoLike)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(l => l.UserIdWhoLike);

            builder.HasMany(u => u.BookNoteDislikes)
               .WithOne(l => l.UserWhoDislike)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasForeignKey(l => l.UserIdWhoDislike);

            builder.HasMany(u => u.BookOpinionLikes)
               .WithOne(l => l.UserWhoLike)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasForeignKey(l => l.UserIdWhoLike);

            builder.HasMany(u => u.BookOpinionDislikes)
               .WithOne(l => l.UserWhoDislike)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasForeignKey(l => l.UserIdWhoDislike);

            builder.HasMany(u => u.BookOpinions)
               .WithOne(l => l.User)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasForeignKey(l => l.UserId);

            builder.Property(u => u.Description).HasMaxLength(100);
        }
    }
}
