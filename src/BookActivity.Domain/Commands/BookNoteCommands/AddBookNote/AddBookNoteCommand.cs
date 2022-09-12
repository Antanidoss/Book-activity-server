using BookActivity.Domain.Models;
using System;

namespace BookActivity.Domain.Commands.BookNoteCommands.AddBookNote
{
    public class AddBookNoteCommand : BookNoteCommand
    {
        public AddBookNoteCommand(Guid activeBookId, string note, NoteColor noteColor)
        {
            ActiveBookId = activeBookId;
            Note = note;
            NoteColor = noteColor;
        }
    }
}
