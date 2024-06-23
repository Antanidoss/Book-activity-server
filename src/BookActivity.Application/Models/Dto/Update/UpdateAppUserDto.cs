using System;

namespace BookActivity.Application.Models.Dto.Update
{
    public class UpdateUserDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string Validate()
        {
            return string.Empty;
        }
    }
}
