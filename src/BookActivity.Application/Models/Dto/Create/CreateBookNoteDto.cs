using System;

namespace BookActivity.Application.Models.Dto.Create
{
    public class CreateBookNoteDto : BaseCreateDto
    {
        public Guid ActiveBookId { get; set; }
        public string Note { get; set; }
        public string NoteColor { get; set; }

        public override void Validate()
        {
            
        }
    }
}
