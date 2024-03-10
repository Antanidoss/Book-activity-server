using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.Dto.Create
{
    public sealed class CreateBookDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Guid> AuthorIds { get; set; }
        public IEnumerable<Guid> CategoryIds { get; set; }
        public IFormFile Image { get; set; }

        public override string Validate()
        {
            return string.Empty;
        }
    }
}
