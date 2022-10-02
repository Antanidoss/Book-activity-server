using BookActivity.Application.Models.Dto.Read;
using System.Collections.Generic;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class BookRatingDto : BaseEntityDto
    {
        public IList<BookOpinionDto> BookOpinions { get; set; }
        public int AverageRating { get; set; }
    }
}
