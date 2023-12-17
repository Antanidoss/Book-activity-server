using BookActivity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookActivity.Infrastructure.Data.EF.Configuration
{
    internal sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne(n => n.ToUser)
                .WithMany(u => u.Notifications)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
