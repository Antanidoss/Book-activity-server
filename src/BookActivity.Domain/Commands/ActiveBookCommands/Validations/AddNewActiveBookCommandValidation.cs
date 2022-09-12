using BookActivity.Domain.Commands.ActiveBookCommands.AddActiveBook;

namespace BookActivity.Domain.Commands.ActiveBookCommands.Validations
{
    public sealed class AddNewActiveBookCommandValidation : ActiveBookValidation<AddActiveBookCommand>
    {
        public AddNewActiveBookCommandValidation()
        {
            ValidateNumberPagesRead();
            ValidateTotalNumberPages();
            ValidateUserId();
        }
    }
}