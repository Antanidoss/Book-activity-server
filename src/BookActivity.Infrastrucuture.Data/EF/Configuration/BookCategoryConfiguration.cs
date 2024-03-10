using BookActivity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookActivity.Infrastructure.Data.EF.Configuration
{
    internal sealed class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.Property(c => c.Title).HasMaxLength(100).IsRequired();
            builder.HasIndex(c => c.Title).IsUnique();
        }
    }
}
