using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Read
{
    public class BookOpinionDTO : BaseEntityDTO
    {
        public int Grade { get; set; }
        public string Description { get; set; }
        public AppUserDTO User { get; set; }
        public BookDTO Book { get; set; }
        public ICollection<ResponseOpinionDTO> ResponseOpinions { get; set; }
        public BookOpinionDTO() : base() { }
        public BookOpinionDTO(
            Guid bookOpinionId,
            DateTime timeOfCreation,
            DateTime timeOfUpdate,
            bool isPublic,
            int grade,
            string description,
            AppUserDTO user,
            BookDTO book,
            ICollection<ResponseOpinionDTO> responseOpinions) : base(bookOpinionId, timeOfCreation, timeOfUpdate, isPublic)
        {
            Grade = grade;
            Description = description;
            User = user;
            Book = book;
            ResponseOpinions = responseOpinions;
        }
    }
}