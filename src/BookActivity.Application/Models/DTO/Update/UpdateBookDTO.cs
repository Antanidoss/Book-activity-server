using System;

namespace BookActivity.Application.Models.Dto.Update
{
    public sealed class UpdateBookDto
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public Guid UserId { get; set; }
    }
}
