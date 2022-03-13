using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Read
{
    public class BookCategoryDTO : BaseEntityDTO
    {
        public string Title { get; set; }
        public ICollection<BookDTO> Books { get; set; }
        public BookCategoryDTO() : base() { }
        public BookCategoryDTO(
            Guid bookCategoryId,
            DateTime timeOfCreation,
            DateTime timeOfUpdate,
            bool isPublic,
            string title,
            ICollection<BookDTO> books) : base (bookCategoryId, timeOfCreation, timeOfUpdate, isPublic)
        {
            Title = title;
            Books = books;
        }
    }
}