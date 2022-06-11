using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Read
{
    public sealed class ActiveBookDTO : BaseEntityDTO
    {
        public int TotalNumberPages { get; set; }
        public int NumberPagesRead { get; set; }
        public string BookName { get; set; }
        public string BookId { get; set; }
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
            string bookName,
            string userName,
            Guid userId,
            ICollection<BookNoteDTO> bookNotes) : base(activeBookId, timeOfCreation, timeOfUpdate, isPublic)
        {
            TotalNumberPages = totalNumberPages;
            NumberPagesRead = numberPagesRead;
            UserName = userName;
            UserId = userId;
            BookNotes = bookNotes;
            BookName = bookName;
        }
    }
}