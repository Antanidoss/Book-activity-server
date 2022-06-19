using BookActivity.Application.Models.DTO.Read;
using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Update
{
    public sealed class UpdateBookDTO
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<BookOpinionDTO> BookOpinions { get; set; }
        public UpdateBookDTO(Guid bookId, string title, string description, IEnumerable<BookOpinionDTO> bookOpinions)
        {
            BookId = bookId;
            Title = title;
            Description = description;
            BookOpinions = bookOpinions;
        }
    }
}
