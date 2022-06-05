using BookActivity.Domain.Commands.ActiveBookCommands.Validations;
using System;

namespace BookActivity.Domain.Commands.ActiveBookCommands
{
    public sealed class UpdateActiveBookCommand : ActiveBookCommand
    {
        public UpdateActiveBookCommand(Guid activeBookId, int numberPagesRead)
        {
            Id = activeBookId;
            NumberPagesRead = numberPagesRead;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateActiveBookCommandValidate().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}