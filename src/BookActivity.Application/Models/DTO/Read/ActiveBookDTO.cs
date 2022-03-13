using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Read
{
    public class ActiveBookDTO : BaseEntityDTO
    {
        public int TotalNumberPages { get; set; }
        public int NumberPagesRead { get; set; }
        public BookDTO Book { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public ICollection<BookNoteDTO> BookNotes { get; set; }
        public ActiveBookDTO() : base() { }
        public ActiveBookDTO(
            Guid activeBookId,
            DateTime timeOfCreation,
            DateTime timeOfUpdate,
            bool isPublic,
            int totalNumberPages,
            int numberPagesRead,
            BookDTO book,
            string userName,
            Guid userId,
            ICollection<BookNoteDTO> bookNotes) : base(activeBookId, timeOfCreation, timeOfUpdate, isPublic)
        {
            TotalNumberPages = totalNumberPages;
            NumberPagesRead = numberPagesRead;
            Book = book;
            UserName = userName;
            UserId = userId;
            BookNotes = bookNotes;
        }
    }
}