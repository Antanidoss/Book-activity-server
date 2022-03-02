namespace BookActivity.Domain.Commands.BookActiveCommands.Validations
{
    public class RemoveActiveBookCommandValidation : ActiveBookValidation<RemoveActiveBookCommand>
    {
        public RemoveActiveBookCommandValidation()
        {
            ValidateActiveBookId();
        }
    }
}
