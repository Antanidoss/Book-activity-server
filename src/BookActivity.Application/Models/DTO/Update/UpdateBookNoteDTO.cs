using System;

namespace BookActivity.Application.Models.DTO.Update
{
    public sealed class UpdateBookNoteDTO
    {
        public Guid BookNoteId { get; set; }
        public string Note { get; set; }
        public NoteColor NoteColor { get; set; }
    }
}
