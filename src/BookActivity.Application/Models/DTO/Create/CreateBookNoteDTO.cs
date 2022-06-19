using System;

namespace BookActivity.Application.Models.DTO.Create
{
    public class CreateBookNoteDTO : BaseCreateDTO
    {
        public Guid ActiveBookId { get; set; }
        public string Note { get; set; }
        public NoteColor NoteColor { get; set; }
    }
}
