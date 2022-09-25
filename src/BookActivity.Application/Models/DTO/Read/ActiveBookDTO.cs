using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Read
{
    public sealed class ActiveBookDto : BaseEntityDto
    {
        public int TotalNumberPages { get; set; }
        public int NumberPagesRead { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public ICollection<BookNoteDto> BookNotes { get; set; }
        public BookDto Book { get; set; }
        public ActiveBookDto() : base() { }
    }
}