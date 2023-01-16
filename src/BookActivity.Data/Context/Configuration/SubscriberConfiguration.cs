using BookActivity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookActivity.Infrastructure.Data.Context.Configuration
{
    internal sealed class SubscriberConfiguration : IEntityTypeConfiguration<Subscriber>
    {
        public void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            builder.Ignore(s => s.Id)
                .HasKey(s => new { s.SubscribedUserId, s.UserIdWhoSubscribed });
        }
    }
}
