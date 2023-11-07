using System;

namespace BookActivity.Application.Models.Dto.Update
{
    public sealed class UpdateBookNoteDto : BaseDto
    {
        public Guid BookNoteId { get; set; }
        public string Note { get; set; }
        public NoteColor NoteColor { get; set; }

        public override string Validate()
        {
            return string.Empty;
        }
    }
}
