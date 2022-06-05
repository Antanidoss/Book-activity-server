using System;
using System.Text.Json.Serialization;

namespace BookActivity.Application.Models.DTO.Create
{
    public sealed class CreateActiveBookDTO : BaseCreateDTO
    {
        public int TotalNumberPages { get; set; }
        public int NumberPagesRead { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; } 
        public bool IsPublic { get; set; }
        public CreateActiveBookDTO() { }
        public CreateActiveBookDTO(int totalNumberPages, int numberPagesRead, Guid bookId, bool isPublic)
        {
            TotalNumberPages = totalNumberPages;
            NumberPagesRead = numberPagesRead;
            BookId = bookId;
            IsPublic = isPublic;
        }
    }
}