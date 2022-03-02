using BookActivity.Domain.Commands.BookActiveCommands.Validations;
using System;

namespace BookActivity.Domain.Commands.BookActiveCommands
{
    public class RemoveActiveBookCommand : ActiveBookCommand
    {
        public RemoveActiveBookCommand(Guid activeBookId)
        {
            Id = activeBookId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveActiveBookCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}