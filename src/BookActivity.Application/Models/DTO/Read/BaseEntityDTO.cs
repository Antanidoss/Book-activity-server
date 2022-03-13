using System;

namespace BookActivity.Application.Models.DTO.Read
{
    public class BaseEntityDTO
    {
        public Guid Id { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public DateTime TimeOfUpdate { get; set; }
        public bool IsPublic { get; set; }
        public BaseEntityDTO() { }
        public BaseEntityDTO(Guid id, DateTime timeOfCreation, DateTime timeOfUpdate, bool isPublic)
        {
            Id = id;
            TimeOfCreation = timeOfCreation;
            TimeOfUpdate = timeOfUpdate;
            IsPublic = isPublic;
        }
    }
}