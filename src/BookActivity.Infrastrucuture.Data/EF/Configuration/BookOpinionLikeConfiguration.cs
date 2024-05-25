using BookActivity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookActivity.Infrastructure.Data.EF.Configuration
{
    internal sealed class BookOpinionLikeConfiguration : IEntityTypeConfiguration<BookOpinionLike>
    {
        public void Configure(EntityTypeBuilder<BookOpinionLike> builder)
        {
            builder.Ignore(s => s.Id).HasKey(l => new { l.UserIdWhoLike, l.BookId, l.UserIdOpinion });
        }
    }
}
