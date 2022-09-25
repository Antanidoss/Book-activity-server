using System;
using System.Collections.Generic;

namespace BookActivity.Application.Models.DTO.Read
{
    public sealed class BookOpinionDto : BaseEntityDto
    {
        public int Grade { get; set; }
        public string Description { get; set; }
        public AppUserDto User { get; set; }
        public BookDto Book { get; set; }
        public ICollection<ResponseOpinionDto> ResponseOpinions { get; set; }
        public BookOpinionDto() : base() { }
        public BookOpinionDto(
            Guid bookOpinionId,
            DateTime timeOfCreation,
            DateTime timeOfUpdate,
            bool isPublic,
            int grade,
            string description,
            AppUserDto user,
            BookDto book,
            ICollection<ResponseOpinionDto> responseOpinions) : base(bookOpinionId, timeOfCreation, timeOfUpdate, isPublic)
        {
            Grade = grade;
            Description = description;
            User = user;
            Book = book;
            ResponseOpinions = responseOpinions;
        }
    }
}