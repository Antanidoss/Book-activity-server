using BookActivity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookActivity.Infrastructure.Data.EF.Configuration
{
    internal sealed class BookOpinionConfiguration : IEntityTypeConfiguration<BookOpinion>
    {
        public void Configure(EntityTypeBuilder<BookOpinion> builder)
        {
            builder.Property(o => o.Description).HasMaxLength(1000).IsRequired();
        }
    }
}
