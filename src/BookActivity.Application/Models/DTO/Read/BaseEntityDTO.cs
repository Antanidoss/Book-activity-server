using System;

namespace BookActivity.Application.Models.DTO.Read
{
    public class BaseEntityDto
    {
        public Guid Id { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public DateTime TimeOfUpdate { get; set; }
        public bool IsPublic { get; set; }
        public BaseEntityDto() { }
        public BaseEntityDto(Guid id, DateTime timeOfCreation, DateTime timeOfUpdate, bool isPublic)
        {
            Id = id;
            TimeOfCreation = timeOfCreation;
            TimeOfUpdate = timeOfUpdate;
            IsPublic = isPublic;
        }
    }
}