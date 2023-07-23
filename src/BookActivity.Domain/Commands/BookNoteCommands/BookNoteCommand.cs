using NetDevPack.Messaging;
using System;

namespace BookActivity.Domain.Commands.BookNoteCommands
{
    public abstract class BookNoteCommand : Command
    {
        public Guid BookNoteId { get; set; }
        public Guid ActiveBookId { get; set; }
        public string Note { get; set; }
        public string NoteColor { get; set; }
    }
}
