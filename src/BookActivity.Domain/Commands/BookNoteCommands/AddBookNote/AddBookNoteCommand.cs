using System;

namespace BookActivity.Domain.Commands.BookNoteCommands.AddBookNote
{
    public class AddBookNoteCommand : BookNoteCommand
    {
        public AddBookNoteCommand(Guid activeBookId, string note, string noteColor, string noteTextColor)
        {
            ActiveBookId = activeBookId;
            Note = note;
            NoteColor = noteColor;
            NoteTextColor = noteTextColor;
        }
    }
}
