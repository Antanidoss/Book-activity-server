namespace BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook
{
    public class AddActiveBookCommand : ActiveBookCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new AddActiveBookCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}