using BookActivity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookActivity.Infrastructure.Data.EF.Configuration
{
    internal sealed class BookNoteLikeConfiguration : IEntityTypeConfiguration<BookNoteLike>
    {
        public void Configure(EntityTypeBuilder<BookNoteLike> builder)
        {
            builder.Ignore(s => s.Id).HasKey(l => new { l.UserIdWhoLike, l.BookNoteId });
        }
    }
}
