using Microsoft.AspNetCore.Http;
using System;

namespace BookActivity.Application.Models.DTO.Update
{
    public sealed class UpdateBookDTO
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
