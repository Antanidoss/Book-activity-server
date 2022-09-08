using BookActivity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookActivity.Infrastructure.Data.Context.Configuration
{
    public class BookOptionConfiguration : IEntityTypeConfiguration<BookOpinion>
    {
        public void Configure(EntityTypeBuilder<BookOpinion> builder)
        {
            builder.ToTable("BookOpinions");
            builder.HasKey(b => b.Id);

            builder.Property(r => r.BookRatingId).IsRequired();
            builder.Property(r => r.UserId).IsRequired();
            builder.Property(r => r.Description).IsRequired();
            builder.Property(r => r.Grade).IsRequired();

            builder.HasOne(b => b.BookRating)
                .WithMany(b => b.BookOpinions)
                .HasForeignKey(b => b.BookRatingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.User)
                .WithMany(b => b.BookOpinions)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}