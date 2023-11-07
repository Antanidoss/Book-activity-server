using System;

namespace BookActivity.Application.Models.Dto.Update
{
    public sealed class UpdateBookDto : BaseDto
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public Guid UserId { get; set; }

        public override string Validate()
        {
            return string.Empty;
        }
    }
}
