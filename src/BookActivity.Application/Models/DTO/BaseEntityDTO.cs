using System;

namespace BookActivity.Application.Models.DTO
{
    public class BaseEntityDTO
    {
        public Guid Id { get; set; }
        public DateTime TimeOfCreation { get; set; }
        public DateTime TimeOfUpdate { get; set; }
        public bool IsPublic { get; set; }
    }
}