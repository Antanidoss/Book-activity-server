using System;

namespace BookActivity.Application.Models.Dto.Create
{
    public sealed class CreateActiveBookDto : BaseDto
    {
        public int TotalNumberPages { get; set; }
        public int NumberPagesRead { get; set; }
        public Guid BookId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Guid UserId { get; set; }

        public override string Validate()
        {
            var errorMessage = string.Empty;

            if (BookId == Guid.Empty)
                errorMessage = $"{nameof(BookId)} сan't be empty";

            if (NumberPagesRead < 0)
                errorMessage = $"{nameof(NumberPagesRead)} сan't be less than zero";

            if (NumberPagesRead > TotalNumberPages)
                errorMessage = $"{nameof(NumberPagesRead)} there can't be more {TotalNumberPages}";

            if (TotalNumberPages <= 0)
                errorMessage = $"{nameof(TotalNumberPages)} сannot be equal to or less than";

            return errorMessage;
        }
    }
}
