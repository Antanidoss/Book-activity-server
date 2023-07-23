using System;

namespace BookActivity.Domain.Models
{
    public sealed class BookNote : BaseEntity
    {
        /// <summary>
        /// Text note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Note color
        /// </summary>
        public string NoteColor { get; set; }

        /// <summary>
        /// Relation of book note with the active book
        /// </summary>
        public ActiveBook ActiveBook { get; private set; }
        public Guid ActiveBookId { get; private set; }

        public BookNote() : base() { }
        public BookNote(string note, string noteColor, Guid activeBookId)
        {
            Note = note;
            NoteColor = noteColor;
            ActiveBookId = activeBookId;
        }
    }
}
