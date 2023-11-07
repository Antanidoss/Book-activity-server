using System;

namespace BookActivity.Application.Models.Dto.Update
{
    public sealed class UpdateNumberPagesReadDto : BaseDto
    {
        public Guid ActiveBookId { get; set; }
        public int NumberPagesRead { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid UserId { get; set; }

        public override string Validate()
        {
            var errorMessage = string.Empty;

            if (ActiveBookId == Guid.Empty)
                errorMessage = $"{nameof(ActiveBookId)} сan't be empty";

            if (NumberPagesRead < 0)
                errorMessage = $"{nameof(ActiveBookId)} сan't be less than zero";

            return errorMessage;
        }
    }
}
