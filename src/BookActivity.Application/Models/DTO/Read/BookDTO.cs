using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Read
{
    public sealed class BookDTO : BaseEntityDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<BookAuthorDTO> Authors { get; set; }
        public IList<BookOpinionDTO> BookOpinions { get; set; }
        public byte[] ImageData { get; set; }
        public bool IsActiveBook { get; set; }

        public BookDTO() : base() { }
    }
}