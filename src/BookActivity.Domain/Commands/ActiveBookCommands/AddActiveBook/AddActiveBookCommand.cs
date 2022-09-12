using BookActivity.Domain.Commands.ActiveBookCommands.Validations;

namespace BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook
{
    public class AddActiveBookCommand : ActiveBookCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new AddNewActiveBookCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}