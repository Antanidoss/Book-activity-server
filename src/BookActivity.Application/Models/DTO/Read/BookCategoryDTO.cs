using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class BookCategoryDto : BaseEntityDto
    {
        public string Title { get; set; }
        public ICollection<BookDto> Books { get; set; }
        public BookCategoryDto() : base() { }
        public BookCategoryDto(
            Guid bookCategoryId,
            DateTime timeOfCreation,
            DateTime timeOfUpdate,
            bool isPublic,
            string title,
            ICollection<BookDto> books) : base (bookCategoryId, timeOfCreation, timeOfUpdate, isPublic)
        {
            Title = title;
            Books = books;
        }
    }
}