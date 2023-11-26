using System;

namespace BookActivity.Application.Models.Dto.Update
{
    public sealed class AddBookOpinionDto : BaseDto
    {
        public Guid BookId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Guid UserId { get; set; }
        public float Grade { get; set; }
        public string Description { get; set; }

        public override string Validate()
        {
            return string.Empty;
        }
    }
}
