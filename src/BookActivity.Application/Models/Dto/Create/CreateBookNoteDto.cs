using System;

namespace BookActivity.Application.Models.Dto.Create
{
    public class CreateBookNoteDto : BaseDto
    {
        public Guid ActiveBookId { get; set; }
        public string Note { get; set; }
        public string NoteColor { get; set; }
        public string NoteTextColor { get; set; }

        public override string Validate()
        {
            return string.Empty;
        }
    }
}
