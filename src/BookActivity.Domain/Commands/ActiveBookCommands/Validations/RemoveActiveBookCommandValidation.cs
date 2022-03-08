namespace BookActivity.Domain.Commands.ActiveBookCommands.Validations
{
    public class RemoveActiveBookCommandValidation : ActiveBookValidation<RemoveActiveBookCommand>
    {
        public RemoveActiveBookCommandValidation()
        {
            ValidateActiveBookId();
        }
    }
}
