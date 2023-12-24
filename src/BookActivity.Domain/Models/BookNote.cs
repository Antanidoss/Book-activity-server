using BookActivity.Domain.Core;
using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Models
{
    public sealed class BookNote : BaseEntity
    {
        public string Note { get; set; }
        public string NoteColor { get; set; }
        public ActiveBook ActiveBook { get; private set; }
        public Guid ActiveBookId { get; private set; }
        public string NoteTextColor { get; set; }
        public ICollection<BookNoteLike> Likes { get; private set; }
        public ICollection<BookNoteDislike> Dislikes { get; private set; }

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
