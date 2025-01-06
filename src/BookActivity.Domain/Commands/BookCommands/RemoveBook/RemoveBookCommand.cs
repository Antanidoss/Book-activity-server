using System;

namespace BookActivity.Domain.Commands.BookCommands.RemoveBook
{
    public sealed class RemoveBookCommand : BookCommand
    {
        public readonly Guid UserId;
        public RemoveBookCommand(Guid bookId, Guid userId)
        {
            BookId = bookId;
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveBookCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}