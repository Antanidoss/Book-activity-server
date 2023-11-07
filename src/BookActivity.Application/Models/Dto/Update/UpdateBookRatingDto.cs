using BookActivity.Application.Models.Dto.Create;
using System;

namespace BookActivity.Application.Models.Dto.Update
{
    public sealed class UpdateBookRatingDto : BaseDto
    {
        public Guid BookRatingId { get; set; }
        public CreateBookOpinionDto BookOpinion { get; set; }

        public override string Validate()
        {
            return string.Empty;
        }
    }
}
