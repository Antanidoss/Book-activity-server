using BookActivity.Domain.Commands.ActiveBookCommands.RemoveActiveBook;

namespace BookActivity.Domain.Commands.ActiveBookCommands.Validations
{
    public sealed class RemoveActiveBookCommandValidation : ActiveBookValidation<RemoveActiveBookCommand>
    {
        public RemoveActiveBookCommandValidation()
        {
            ValidateActiveBookId();
        }
    }
}
