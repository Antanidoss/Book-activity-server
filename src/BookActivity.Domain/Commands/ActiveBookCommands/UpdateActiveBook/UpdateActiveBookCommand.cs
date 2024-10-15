using BookActivity.Domain.Commands.ActiveBookCommands.UpdateActiveBook;

namespace BookActivity.Domain.Commands.ActiveBookCommands
{
    public sealed class UpdateActiveBookCommand : ActiveBookCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new UpdateActiveBookCommandValidate().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}