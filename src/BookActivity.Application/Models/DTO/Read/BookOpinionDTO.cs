using System;

namespace BookActivity.Application.Models.Dto.Read
{
    public sealed class BookOpinionDto
    {
        public Guid Id { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
    }
}