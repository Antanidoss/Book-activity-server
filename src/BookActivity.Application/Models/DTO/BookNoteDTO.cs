using BookActivity.Domain.Models;

namespace BookActivity.Application.Models.DTO
{
    public class BookNoteDTO : BaseEntityDTO
    {
        public string Note { get; set; }
        public NoteColor NoteColor { get; set; }
        public ActiveBookDTO ActiveBook { get; set; }
    }
}