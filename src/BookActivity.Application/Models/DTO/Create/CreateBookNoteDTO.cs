using System;

namespace BookActivity.Application.Models.Dto.Create
{
    public class CreateBookNoteDto : BaseCreateDto
    {
        public Guid ActiveBookId { get; set; }
        public string Note { get; set; }
        public NoteColor NoteColor { get; set; }
    }
}
