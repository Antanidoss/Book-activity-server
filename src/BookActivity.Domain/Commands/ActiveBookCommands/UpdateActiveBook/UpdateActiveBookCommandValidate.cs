using BookActivity.Domain.Commands.ActiveBookCommands.Validations;

namespace BookActivity.Domain.Commands.ActiveBookCommands.UpdateActiveBook
{
    public sealed class UpdateActiveBookCommandValidate : ActiveBookValidation<UpdateActiveBookCommand>
    {
        public UpdateActiveBookCommandValidate()
        {
            ValidateActiveBookId();
            ValidateNumberPagesRead();
        }
    }
}