using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Read
{
    public class BookDTO : BaseEntityDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<BookAuthorDTO> Authors { get; set; }
        public IList<BookOpinionDTO> BookOpinions { get; set; }
        public BookDTO() : base() { }
        public BookDTO(
            Guid bookId,
            DateTime timeOfCreation,
            DateTime timeOfUpdate,
            bool isPublic,
            string title,
            string description,
            IEnumerable<BookAuthorDTO> authors,
            IList<BookOpinionDTO> bookOpinions) : base(bookId, timeOfCreation, timeOfUpdate, isPublic)
        {
            Title = title;
            Description = description;
            Authors = authors;
            BookOpinions = bookOpinions;
        }
    }
}