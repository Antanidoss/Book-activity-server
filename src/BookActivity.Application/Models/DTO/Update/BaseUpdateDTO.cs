using System;

namespace BookActivity.Application.Models.DTO.Update
{
    public class BaseUpdateDTO
    {
        public Guid Id { get; set; }

        public BaseUpdateDTO() { }
        public BaseUpdateDTO(Guid id)
        {
            Id = id;
        }
    }
}