using BookActivity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookActivity.Infrastructure.Data.Context.Configuration
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
            

        }
    }
}
