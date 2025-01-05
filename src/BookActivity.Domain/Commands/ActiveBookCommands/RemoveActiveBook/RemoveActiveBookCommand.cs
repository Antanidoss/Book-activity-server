using System;

namespace BookActivity.Domain.Commands.ActiveBookCommands.RemoveActiveBook
{
    public sealed class RemoveActiveBookCommand : ActiveBookCommand
    {
        public RemoveActiveBookCommand(Guid activeBookId, Guid currentUserId)
        {
            Id = activeBookId;
            UserId = currentUserId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveActiveBookCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}