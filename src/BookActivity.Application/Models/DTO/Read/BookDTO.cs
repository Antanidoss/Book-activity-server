using System.Collections.Generic;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class BookDto : BaseEntityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<BookAuthorDto> Authors { get; set; }
        public BookRatingDto BookRating { get; set; }
        public byte[] ImageData { get; set; }
        public bool IsActiveBook { get; set; }

        public BookDto() : base() { }
    }
}