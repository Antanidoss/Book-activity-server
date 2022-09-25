using BookActivity.Domain.Models;
using System;

namespace BookActivity.Application.Models.DTO.Read
{
    public sealed class BookNoteDto : BaseEntityDto
    {
        public string Note { get; set; }
        public NoteColor NoteColor { get; set; }
        public ActiveBookDto ActiveBook { get; set; }
        public BookNoteDto() : base() { }
        public BookNoteDto(
            Guid bookNoteId,
            DateTime timeOfCreation,
            DateTime timeOfUpdate,
            bool isPublic,
            string note,
            NoteColor noteColor,
            ActiveBookDto activeBook) : base(bookNoteId, timeOfCreation, timeOfUpdate, isPublic)
        {
            Note = note;
            NoteColor = noteColor;
            ActiveBook = activeBook;
        }
    }
}