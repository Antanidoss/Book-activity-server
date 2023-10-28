using BookActivity.Domain.Core;
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

        /// <summary>
        /// Note text color
        /// </summary>
        public string NoteTextColor { get; set; }

        public BookNote() : base() { }
        public BookNote(string note, string noteColor, Guid activeBookId, string noteTextColor)
        {
            Note = note;
            NoteColor = noteColor;
            ActiveBookId = activeBookId;
            NoteTextColor = noteTextColor;
        }
    }
}
