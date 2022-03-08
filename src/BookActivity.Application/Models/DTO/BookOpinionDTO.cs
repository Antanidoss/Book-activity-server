using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO
{
    public class BookOpinionDTO : BaseEntityDTO
    {
        public int Grade { get; set; }
        public string Description { get; set; }
        public AppUserDTO User { get; set; }
        public BookDTO Book { get; set; }
        public ICollection<ResponseOpinionDTO> ResponseOpinions { get; set; }
    }
}