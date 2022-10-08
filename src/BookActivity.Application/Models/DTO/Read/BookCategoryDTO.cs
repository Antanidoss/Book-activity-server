using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class BookCategoryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<BookDto> Books { get; set; }
    }
}