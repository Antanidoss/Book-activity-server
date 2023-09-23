using System;

namespace BookActivity.Application.Models.Dto.Update
{
    public sealed class UpdateBookNoteDto
    {
        public Guid BookNoteId { get; set; }
        public string Note { get; set; }
        public NoteColor NoteColor { get; set; }
    }
}
