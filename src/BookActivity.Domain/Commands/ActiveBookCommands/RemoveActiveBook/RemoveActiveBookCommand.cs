using BookActivity.Domain.Commands.ActiveBookCommands.Validations;
using NetDevPack.Messaging;
using System;

namespace BookActivity.Domain.Commands.ActiveBookCommands.RemoveActiveBook
{
    public sealed class RemoveActiveBookCommand : ActiveBookCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new RemoveActiveBookCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}