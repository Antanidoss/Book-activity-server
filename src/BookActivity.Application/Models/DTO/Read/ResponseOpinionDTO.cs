using BookActivity.Domain.Models;
using System;

namespace BookActivity.Application.Models.DTO.Read
{
    public class ResponseOpinionDTO : BaseEntityDTO
    {
        public ResponseOpinionType ResponseOpinionType { get; set; }
        public BookOpinionDTO BookOpinion { get; set; }
        public AppUserDTO User { get; set; }
        public ResponseOpinionDTO() : base() { }
        public ResponseOpinionDTO(
            Guid responseOpinionId,
            DateTime timeOfCreation,
            DateTime timeOfUpdate,
            bool isPublic,
            ResponseOpinionType responseOpinionType,
            BookOpinionDTO bookOpinion,
            AppUserDTO user) : base(responseOpinionId, timeOfCreation, timeOfUpdate, isPublic)
        {
            ResponseOpinionType = responseOpinionType;
            BookOpinion = bookOpinion;
            User = user;
        }
    }
}