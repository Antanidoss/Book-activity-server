using BookActivity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookActivity.Infrastructure.Data.EF.Configuration
{
    internal sealed class BookOpinionDislikeConfiguration : IEntityTypeConfiguration<BookOpinionDislike>
    {
        public void Configure(EntityTypeBuilder<BookOpinionDislike> builder)
        {
            builder.Ignore(s => s.Id).HasKey(l => new { l.UserIdWhoDislike, l.BookId, l.UserIdOpinion });
        }
    }
}
