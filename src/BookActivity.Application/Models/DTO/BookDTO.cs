using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO
{
    public class BookDTO : BaseEntityDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<BookAuthorDTO> Authors { get; set; }
        public IList<BookOpinionDTO> BookOpinions { get; set; }
    }
}
