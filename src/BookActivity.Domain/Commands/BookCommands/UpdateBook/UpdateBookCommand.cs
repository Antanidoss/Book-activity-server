using System;

namespace BookActivity.Domain.Commands.BookCommands.UpdateBook
{
    public sealed class UpdateBookCommand : BookCommand
    {
        public Guid UserId { get; init; }

        public override bool IsValid()
        {
            ValidationResult = new UpdateBookCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
