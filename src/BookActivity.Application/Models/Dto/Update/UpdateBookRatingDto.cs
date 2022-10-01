using BookActivity.Application.Models.Dto.Create;
using System;

namespace BookActivity.Application.Models.Dto.Update
{
    public sealed class UpdateBookRatingDto
    {
        public Guid BookRatingId { get; set; }
        public CreateBookOpinionDto BookOpinion { get; set; }
    }
}
