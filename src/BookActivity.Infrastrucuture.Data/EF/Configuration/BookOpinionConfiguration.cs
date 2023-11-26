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
            builder.Property(o => o.Grade).HasMaxLength(BookOpinion.GradeMax);
            builder.Ignore(s => s.Id).HasKey(o => new { o.BookId, o.UserId });
        }
    }
}
