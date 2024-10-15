using BookActivity.Domain.Commands.ActiveBookCommands.Validations;

namespace BookActivity.Domain.Commands.ActiveBookCommands.RemoveActiveBook
{
    public sealed class RemoveActiveBookCommandValidation : ActiveBookValidation<RemoveActiveBookCommand>
    {
        public RemoveActiveBookCommandValidation()
        {
            ValidateActiveBookId();
        }
    }
}
