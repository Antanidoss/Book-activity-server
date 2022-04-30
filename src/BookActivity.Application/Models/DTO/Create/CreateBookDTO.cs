using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Create
{
    public sealed class CreateBookDTO : BaseCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<int> AuthorIds { get; private set; }

        public CreateBookDTO() { }

        public CreateBookDTO(string title, string description, IEnumerable<int> authorIds) : base()
        {
            Title = title;
            Description = description;
            AuthorIds = authorIds;
        }
    }
}
