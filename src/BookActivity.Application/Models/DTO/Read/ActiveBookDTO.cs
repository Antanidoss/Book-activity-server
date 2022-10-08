using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class ActiveBookDto
    {
        public Guid Id { get; set; }
        public int TotalNumberPages { get; set; }
        public int NumberPagesRead { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public ICollection<BookNoteDto> BookNotes { get; set; }
        public BookDto Book { get; set; }
        public ActiveBookDto() : base() { }
    }
}