using System;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class BookNoteDto
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        public NoteColor NoteColor { get; set; }
        public ActiveBookDto ActiveBook { get; set; }
    }
}
