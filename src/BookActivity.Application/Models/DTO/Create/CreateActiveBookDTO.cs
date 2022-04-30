using System;

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
        public CreateActiveBookDTO(int totalNumberPages, int numberPagesRead, Guid bookId, Guid userId, bool isPublic)
        {
            TotalNumberPages = totalNumberPages;
            NumberPagesRead = numberPagesRead;
            BookId = bookId;
            UserId = userId;
            IsPublic = isPublic;
        }
    }
}