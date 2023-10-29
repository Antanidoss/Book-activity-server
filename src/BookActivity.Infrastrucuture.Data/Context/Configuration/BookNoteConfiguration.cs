using BookActivity.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookActivity.Infrastructure.Data.Context.Configuration
{
    internal sealed class BookNoteConfiguration : IEntityTypeConfiguration<BookNote>
    {
        public void Configure(EntityTypeBuilder<BookNote> builder)
        {
            builder.Property(n => n.Note).HasMaxLength(3000).IsRequired();
            builder.Property(n => n.NoteColor).HasMaxLength(7).IsRequired();
            builder.Property(n => n.NoteTextColor).HasMaxLength(7).IsRequired();
        }
    }
}
