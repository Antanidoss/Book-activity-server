using BookActivity.Domain.Commands.ActiveBookCommands.Validations;

namespace BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook
{
    public sealed class AddActiveBookCommandValidation : ActiveBookValidation<AddActiveBookCommand>
    {
        public AddActiveBookCommandValidation()
        {
            ValidateNumberPagesRead();
            ValidateTotalNumberPages();
            ValidateUserId();
        }
    }
}