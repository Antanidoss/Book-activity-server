using BookActivity.Domain.Models;
using NetDevPack.Messaging;
using System;

namespace BookActivity.Domain.Commands.BookNoteCommands
{
    public abstract class BookNoteCommand : Command
    {
        public Guid BookNoteId { get; set; }
        public Guid ActiveBookId { get; set; }
        public string Note { get; set; }
        public NoteColor NoteColor { get; set; }
    }
}
