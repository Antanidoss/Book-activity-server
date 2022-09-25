using BookActivity.Domain.Models;
using System;

namespace BookActivity.Application.Models.DTO.Read
{
    public sealed class ResponseOpinionDto : BaseEntityDto
    {
        public ResponseOpinionType ResponseOpinionType { get; set; }
        public BookOpinionDto BookOpinion { get; set; }
        public AppUserDto User { get; set; }
        public ResponseOpinionDto() : base() { }
        public ResponseOpinionDto(
            Guid responseOpinionId,
            DateTime timeOfCreation,
            DateTime timeOfUpdate,
            bool isPublic,
            ResponseOpinionType responseOpinionType,
            BookOpinionDto bookOpinion,
            AppUserDto user) : base(responseOpinionId, timeOfCreation, timeOfUpdate, isPublic)
        {
            ResponseOpinionType = responseOpinionType;
            BookOpinion = bookOpinion;
            User = user;
        }
    }
}