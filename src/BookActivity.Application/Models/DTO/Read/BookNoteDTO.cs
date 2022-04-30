using BookActivity.Domain.Models;
using System;

namespace BookActivity.Application.Models.DTO.Read
{
    public sealed class BookNoteDTO : BaseEntityDTO
    {
        public string Note { get; set; }
        public NoteColor NoteColor { get; set; }
        public ActiveBookDTO ActiveBook { get; set; }
        public BookNoteDTO() : base() { }
        public BookNoteDTO(
            Guid bookNoteId,
            DateTime timeOfCreation,
            DateTime timeOfUpdate,
            bool isPublic,
            string note,
            NoteColor noteColor,
            ActiveBookDTO activeBook) : base(bookNoteId, timeOfCreation, timeOfUpdate, isPublic)
        {
            Note = note;
            NoteColor = noteColor;
            ActiveBook = activeBook;
        }
    }
}