using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class BookRatingDto
    {
        public Guid Id { get; set; }
        public IList<BookOpinionDto> BookOpinions { get; set; }
        public int AverageRating { get; set; }
    }
}
