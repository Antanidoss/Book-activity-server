using BookActivity.Domain.Models;

namespace BookActivity.Application.Models.DTO
{
    public class ResponseOpinionDTO
    {
        public ResponseOpinionType ResponseOpinionType { get; set; }
        public BookOpinionDTO BookOpinion { get; set; }
        public AppUserDTO User { get; set; }
    }
}